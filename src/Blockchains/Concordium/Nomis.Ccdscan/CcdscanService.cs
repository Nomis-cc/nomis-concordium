// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Microsoft.Extensions.Options;
using Nomis.Blockchain.Abstractions;
using Nomis.Blockchain.Abstractions.Contracts;
using Nomis.Blockchain.Abstractions.Converters;
using Nomis.Blockchain.Abstractions.Extensions;
using Nomis.Blockchain.Abstractions.Models;
using Nomis.Blockchain.Abstractions.Requests;
using Nomis.Blockchain.Abstractions.Stats;
using Nomis.Ccdscan.Calculators;
using Nomis.Ccdscan.Interfaces;
using Nomis.Ccdscan.Interfaces.Extensions;
using Nomis.Ccdscan.Interfaces.Models;
using Nomis.Ccdscan.Interfaces.Requests;
using Nomis.Ccdscan.Settings;
using Nomis.DefiLlama.Interfaces;
using Nomis.Domain.Scoring.Entities;
using Nomis.ScoringService.Interfaces;
using Nomis.SoulboundTokenService.Interfaces;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Ccdscan
{
    /// <inheritdoc cref="IConcordiumScoringService"/>
    internal sealed class CcdscanService :
        BlockchainDescriptor,
        IConcordiumScoringService,
        IScopedService
    {
        private readonly ICcdscanGraphQLClient _client;
        private readonly IScoringService _scoringService;
        private readonly INonEvmSoulboundTokenService _soulboundTokenService;
        private readonly IDefiLlamaService _defiLlamaService;

        /// <summary>
        /// Initialize <see cref="CcdscanService"/>.
        /// </summary>
        /// <param name="settings"><see cref="CcdscanSettings"/>.</param>
        /// <param name="client"><see cref="ICcdscanGraphQLClient"/>.</param>
        /// <param name="scoringService"><see cref="IScoringService"/>.</param>
        /// <param name="soulboundTokenService"><see cref="INonEvmSoulboundTokenService"/>.</param>
        /// <param name="defiLlamaService"><see cref="IDefiLlamaService"/>.</param>
        public CcdscanService(
            IOptions<CcdscanSettings> settings,
            ICcdscanGraphQLClient client,
            IScoringService scoringService,
            INonEvmSoulboundTokenService soulboundTokenService,
            IDefiLlamaService defiLlamaService)
            : base(settings.Value.BlockchainDescriptor)
        {
            _client = client;
            _scoringService = scoringService;
            _soulboundTokenService = soulboundTokenService;
            _defiLlamaService = defiLlamaService;
        }

        /// <summary>
        /// Coingecko native token id.
        /// </summary>
        public string CoingeckoNativeTokenId => "concordium";

        /// <inheritdoc/>
        public async Task<Result<TWalletScore>> GetWalletStatsAsync<TWalletStatsRequest, TWalletScore, TWalletStats, TTransactionIntervalData>(
            TWalletStatsRequest request,
            CancellationToken cancellationToken = default)
            where TWalletStatsRequest : WalletStatsRequest
            where TWalletScore : IWalletScore<TWalletStats, TTransactionIntervalData>, new()
            where TWalletStats : class, IWalletCommonStats<TTransactionIntervalData>, new()
            where TTransactionIntervalData : class, ITransactionIntervalData
        {
            var account = await GetCcdscanAccountAsync(new(request.Address)).ConfigureAwait(false);
            if (!account.Succeeded || account.Data == null)
            {
                return await Result<TWalletScore>.FailAsync("Failed to get account data for this wallet.").ConfigureAwait(false);
            }

            var balance = account
                .Data?
                .Amount;
            decimal usdBalance =
                (await _defiLlamaService.TokensPriceAsync(new List<string?> { $"coingecko:{CoingeckoNativeTokenId}" }).ConfigureAwait(false))?.TokensPrices[$"coingecko:{CoingeckoNativeTokenId}"].Price * (balance ?? new BigInteger(0)).ToC() ?? 0;

            var transactions = account.Data?.Transactions;

            var walletStats = new ConcordiumStatCalculator(
                    request.Address,
                    (decimal)(balance ?? new BigInteger(0)),
                    usdBalance,
                    account.Data!,
                    transactions?.Nodes)
                .GetStats() as TWalletStats;

            double score = walletStats!.GetScore<TWalletStats, TTransactionIntervalData>();
            var scoringData = new ScoringData(request.Address, request.Address, ChainId, score, JsonSerializer.Serialize(walletStats));
            await _scoringService.SaveScoringDataToDatabaseAsync(scoringData, cancellationToken).ConfigureAwait(false);

            // getting signature
            ushort mintedScore = (ushort)(score * 10000);
            var signatureResult = _soulboundTokenService.GetSoulboundTokenSignature(new()
            {
                Score = mintedScore,
                ScoreType = request.ScoreType,
                To = request.Address,
                Nonce = request.Nonce,
                ChainId = ChainId,
                ContractAddress = SBTContractAddresses?.ContainsKey(request.ScoreType) == true ? SBTContractAddresses?[request.ScoreType] : null,
                Deadline = request.Deadline
            });

            var messages = signatureResult.Messages;
            messages.Add($"Got {ChainName} wallet {request.ScoreType.ToString()} score.");
            return await Result<TWalletScore>.SuccessAsync(
                new()
                {
                    Address = request.Address,
                    Stats = walletStats,
                    Score = score,
                    MintedScore = mintedScore,
                    Signature = signatureResult.Data.Signature
                }, messages).ConfigureAwait(false);
        }

        private async Task<TResult> GetDataAsync<TResult>(GraphQLRequest query, params string[] responseAliases)
        {
            var responseAliasList = responseAliases.ToList();
            var response = await _client.SendQueryAsync<JsonObject>(query).ConfigureAwait(false);
            var result = response.Data[responseAliasList.First()];
            responseAliasList.RemoveAt(0);
            foreach (string responseAlias in responseAliasList)
            {
                result = result![responseAlias];
            }

            var data = JsonSerializer.Deserialize<TResult>(result!.ToJsonString(), new JsonSerializerOptions
            {
                Converters = { new BigIntegerConverter() }
            }) !;

            return data;
        }

        private async Task<Result<CcdscanAccount?>> GetCcdscanAccountAsync(
            CcdscanTransactionsRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = """
                query Search($query: String!, $first: Int!, $after: String) {
                  search(query: $query) {
                    accounts(first: 1) {
                      nodes {
                        id
                        amount
                        createdAt
                        address {
                          asString
                        }
                        transactions(first: $first, after: $after) {
                          nodes{
                            transaction {
                              id
                              transactionHash
                              senderAccountAddress {
                                asString
                              }
                              block {
                                id
                                blockHash
                                blockHeight
                                blockSlotTime
                              }
                              ccdCost
                              energyCost
                              result {
                                __typename
                              }
                            }
                          }
                          pageInfo {
                            hasNextPage
                            endCursor
                          }
                        }
                      }
                    }
                  }
                }
                """,
                Variables = request
            };

            var result = new List<CcdscanTransactionNode>();
            var data = await GetDataAsync<List<CcdscanAccount>>(query, "search", "accounts", "nodes").ConfigureAwait(false);
            result.AddRange(data.FirstOrDefault()?.Transactions?.Nodes!);
            while (data.FirstOrDefault()?.Transactions?.PageInfo?.HasNextPage == true)
            {
                request = new CcdscanTransactionsRequest(request.Query!, 50, data.FirstOrDefault()?.Transactions?.PageInfo?.EndCursor);
                query.Variables = request;
                data = await GetDataAsync<List<CcdscanAccount>>(query, "search", "accounts", "nodes").ConfigureAwait(false);
                result.AddRange(data.FirstOrDefault()?.Transactions?.Nodes!);
            }

            var response = data.FirstOrDefault();
            if (response != null)
            {
                response.Transactions = new CcdscanTransactions
                {
                    Nodes = result
                };
            }

            return await Result<CcdscanAccount?>.SuccessAsync(response, "Transactions received.").ConfigureAwait(false);
        }
    }
}