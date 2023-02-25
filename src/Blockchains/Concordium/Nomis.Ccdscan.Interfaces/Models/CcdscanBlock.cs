// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanBlock.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan block data.
    /// </summary>
    public class CcdscanBlock
    {
        /// <summary>
        /// Id.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Block hash.
        /// </summary>
        [JsonPropertyName("blockHash")]
        public string? BlockHash { get; set; }

        /// <summary>
        /// Block slot time.
        /// </summary>
        [JsonPropertyName("blockSlotTime")]
        public DateTime BlockSlotTime { get; set; }
    }
}