// ------------------------------------------------------------------------------------------------------
// <copyright file="ConcordiumStatCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Blockchain.Abstractions.Calculators;
using Nomis.Blockchain.Abstractions.Models;
using Nomis.Ccdscan.Interfaces.Extensions;
using Nomis.Ccdscan.Interfaces.Models;

namespace Nomis.Ccdscan.Calculators
{
    /// <summary>
    /// Concordium wallet stats calculator.
    /// </summary>
    internal sealed class ConcordiumStatCalculator :
        IStatCalculator<ConcordiumWalletStats, ConcordiumTransactionIntervalData>
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly decimal _usdBalance;
        private readonly CcdscanAccount _account;
        private readonly IEnumerable<CcdscanTransactionNode>? _transactions;

        public ConcordiumStatCalculator(
            string address,
            decimal balance,
            decimal usdBalance,
            CcdscanAccount account,
            IEnumerable<CcdscanTransactionNode>? transactions)
        {
            _address = address;
            _balance = balance;
            _usdBalance = usdBalance;
            _account = account;
            _transactions = transactions;
        }

        public ConcordiumWalletStats GetStats()
        {
            if (_transactions?.Any() != true)
            {
                return new()
                {
                    NoData = true
                };
            }

            int walletAge = (int)((DateTime.UtcNow - _account.CreatedAt).TotalDays / 30);
            if (walletAge == 0)
            {
                walletAge = 1;
            }

            var intervals = IStatCalculator
                .GetTransactionsIntervals(_transactions.Select(x => x.Transaction!.Block!.BlockSlotTime)).ToList();
            if (intervals.Count == 0)
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);
            var yearAgo = DateTime.Now.AddYears(-1);

            var turnoverIntervalsDataList = _transactions
                .Select(x => new TurnoverIntervalsData(
                x.Transaction!.Block!.BlockSlotTime,
                x.Transaction.CcdCost ?? new BigInteger(0),
                x.Transaction?.SenderAccountAddress?.Address?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) != true));
            var turnoverIntervals = IStatCalculator<ConcordiumTransactionIntervalData>
                .GetTurnoverIntervals(turnoverIntervalsDataList, _transactions.Min(x => x.Transaction!.Block!.BlockSlotTime)).ToList();

            return new()
            {
                NativeBalance = _balance.ToC(),
                NativeBalanceUSD = _usdBalance,
                WalletAge = walletAge,
                TotalTransactions = _transactions.Count(),
                TotalRejectedTransactions = _transactions.Count(t => t.Transaction?.Result?.Result?.Equals("Success", StringComparison.InvariantCultureIgnoreCase) != true),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = turnoverIntervals.Sum(x => Math.Abs((decimal)x.AmountSum)).ToC(),
                TurnoverIntervals = turnoverIntervals,
                BalanceChangeInLastMonth = IStatCalculator<ConcordiumTransactionIntervalData>.GetBalanceChangeInLastMonth(turnoverIntervals),
                BalanceChangeInLastYear = IStatCalculator<ConcordiumTransactionIntervalData>.GetBalanceChangeInLastYear(turnoverIntervals),
                LastMonthTransactions = _transactions.Count(x => x.Transaction!.Block!.BlockSlotTime > monthAgo),
                LastYearTransactions = _transactions.Count(x => x.Transaction!.Block!.BlockSlotTime > yearAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.OrderBy(x => x.Transaction!.Block!.BlockSlotTime).Last().Transaction!.Block!.BlockSlotTime).TotalDays / 30),
            };
        }
    }
}