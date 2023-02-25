// ------------------------------------------------------------------------------------------------------
// <copyright file="BasescanAccountERC1155TokenEvents.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Basescan.Interfaces.Models
{
    /// <summary>
    /// Basescan account ERC-1155 token transfer events.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BasescanAccountERC1155TokenEvents :
        IBasescanTransferList<BasescanAccountERC1155TokenEvent>
    {
        /// <summary>
        /// Status.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Message.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Account ERC-1155 token event list.
        /// </summary>
        [JsonPropertyName("result")]
        [DataMember(EmitDefaultValue = true)]
        public IList<BasescanAccountERC1155TokenEvent>? Data { get; set; } = new List<BasescanAccountERC1155TokenEvent>();
    }
}