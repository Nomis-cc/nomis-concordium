// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan transaction data.
    /// </summary>
    public class CcdscanTransaction
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Transaction hash.
        /// </summary>
        [JsonPropertyName("transactionHash")]
        public string? TransactionHash { get; set; }

        /// <summary>
        /// Block.
        /// </summary>
        [JsonPropertyName("block")]
        public CcdscanBlock? Block { get; set; }

        /// <summary>
        /// Sender account address.
        /// </summary>
        [JsonPropertyName("senderAccountAddress")]
        public CcdscanAddress? SenderAccountAddress { get; set; }

        /// <summary>
        /// CCD cost.
        /// </summary>
        [JsonPropertyName("ccdCost")]
        public BigInteger? CcdCost { get; set; }

        /// <summary>
        /// Energy cost.
        /// </summary>
        [JsonPropertyName("energyCost")]
        public BigInteger? EnergyCost { get; set; }

        /// <summary>
        /// Transaction result.
        /// </summary>
        [JsonPropertyName("result")]
        public CcdscanResult? Result { get; set; }
    }
}