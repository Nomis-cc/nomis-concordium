// ------------------------------------------------------------------------------------------------------
// <copyright file="ConcordiumWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Nomis.Blockchain.Abstractions.Models;
using Nomis.Blockchain.Abstractions.Stats;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Concordium wallet stats.
    /// </summary>
    public sealed class ConcordiumWalletStats :
        IWalletCommonStats<ConcordiumTransactionIntervalData>,
        IWalletNativeBalanceStats,
        IWalletTransactionStats
    {
        /// <inheritdoc/>
        public bool NoData { get; init; }

        /// <inheritdoc/>
        public string NativeToken => "Ͼ";

        /// <inheritdoc/>
        [Display(Description = "Wallet native token balance", GroupName = "Native token")]
        public decimal NativeBalance { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Wallet native token balance", GroupName = "USD")]
        public decimal NativeBalanceUSD { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Wallet age", GroupName = "months")]
        public int WalletAge { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Total transactions on wallet", GroupName = "number")]
        public int TotalTransactions { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Total rejected transactions on wallet", GroupName = "number")]
        public int TotalRejectedTransactions { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Average time interval between transactions", GroupName = "hours")]
        public double AverageTransactionTime { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Maximum time interval between transactions", GroupName = "hours")]
        public double MaxTransactionTime { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Minimal time interval between transactions", GroupName = "hours")]
        public double MinTransactionTime { get; init; }

        /// <inheritdoc/>
        [Display(Description = "The movement of funds on the wallet", GroupName = "Native token")]
        public decimal WalletTurnover { get; init; }

        /// <inheritdoc/>
        public IEnumerable<ConcordiumTransactionIntervalData>? TurnoverIntervals { get; init; }

        /// <inheritdoc/>
        [Display(Description = "The balance change value in the last month", GroupName = "Native token")]
        public decimal BalanceChangeInLastMonth { get; init; }

        /// <inheritdoc/>
        [Display(Description = "The balance change value in the last year", GroupName = "Native token")]
        public decimal BalanceChangeInLastYear { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Time since last transaction", GroupName = "months")]
        public int TimeFromLastTransaction { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Last month transactions", GroupName = "number")]
        public int LastMonthTransactions { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Last year transactions on wallet", GroupName = "number")]
        public int LastYearTransactions { get; init; }

        /// <inheritdoc/>
        [Display(Description = "Average transaction per months", GroupName = "number")]
        public double TransactionsPerMonth => WalletAge != 0 ? (double)TotalTransactions / WalletAge : 0;

        /// <inheritdoc/>
        public IDictionary<string, PropertyData> StatsDescriptions => GetType()
            .GetProperties()
            .Where(prop => !ExcludedStatDescriptions.Contains(prop.Name) && Attribute.IsDefined(prop, typeof(DisplayAttribute)) && !Attribute.IsDefined(prop, typeof(JsonIgnoreAttribute)))
            .ToDictionary(p => p.Name, p => new PropertyData(p, NativeToken));

        /// <inheritdoc cref="IWalletCommonStats{ITransactionIntervalData}.ExcludedStatDescriptions"/>
        [JsonIgnore]
        public IEnumerable<string> ExcludedStatDescriptions =>
            IWalletCommonStats<ConcordiumTransactionIntervalData>.ExcludedStatDescriptions.Union(new List<string>());
    }
}