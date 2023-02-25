// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseEvmWalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;
using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.Blockchain.Abstractions.Requests
{
    /// <summary>
    /// Base EVM wallet stats request.
    /// </summary>
    public abstract class BaseEvmWalletStatsRequest :
        WalletStatsRequest,
        IWalletTokensBalancesRequest
    {
        /// <inheritdoc />
        /// <example>true</example>
        [FromQuery]
        [JsonPropertyOrder(-5)]
        public bool GetHoldTokensBalances { get; set; } = true;

        /// <inheritdoc />
        /// <example>6</example>
        [FromQuery]
        [Range(typeof(int), "1", "8760")]
        [JsonPropertyOrder(-4)]
        public int SearchWidthInHours { get; set; } = 6;
    }
}