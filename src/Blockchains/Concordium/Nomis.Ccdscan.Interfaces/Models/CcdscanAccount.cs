// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan account data.
    /// </summary>
    public class CcdscanAccount
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public BigInteger? Amount { get; set; }

        /// <summary>
        /// Created at.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Account address.
        /// </summary>
        [JsonPropertyName("address")]
        public CcdscanAddress? Address { get; set; }

        /// <summary>
        /// Transactions.
        /// </summary>
        [JsonPropertyName("transactions")]
        public CcdscanTransactions? Transactions { get; set; }
    }
}