// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksNonFungibleTokenEvent.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks non fungible token event data.
    /// </summary>
    public class NearblocksNonFungibleTokenEvent
    {
        /// <summary>
        /// Emitted for receipt id.
        /// </summary>
        [JsonPropertyName("emitted_for_receipt_id")]
        public string? EmittedForReceiptId { get; set; }

        /// <summary>
        /// Emitted at block timestamp.
        /// </summary>
        [JsonPropertyName("emitted_at_block_timestamp")]
        public BigInteger EmittedAtBlockTimestamp { get; set; }

        /// <summary>
        /// Emitted by contract account id.
        /// </summary>
        [JsonPropertyName("emitted_by_contract_account_id")]
        public string? EmittedByContractAccountId { get; set; }

        /// <summary>
        /// Token id.
        /// </summary>
        [JsonPropertyName("token_id")]
        public string? TokenId { get; set; }

        /// <summary>
        /// Event kind.
        /// </summary>
        [JsonPropertyName("event_kind")]
        public string? EventKind { get; set; }

        /// <summary>
        /// Token old owner account id.
        /// </summary>
        [JsonPropertyName("token_old_owner_account_id")]
        public string? From { get; set; }

        /// <summary>
        /// Token new owner account id.
        /// </summary>
        [JsonPropertyName("token_new_owner_account_id")]
        public string? To { get; set; }

        /// <summary>
        /// Receipt data.
        /// </summary>
        [JsonPropertyName("receipt")]
        public NearblocksTokenEventReceipt? Receipt { get; set; }

        /// <summary>
        /// Nft meta.
        /// </summary>
        [JsonPropertyName("nft_meta")]
        public NearblocksNonFungibleTokenEventMeta? Meta { get; set; }
    }
}