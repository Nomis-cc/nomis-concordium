// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanTransactions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan transactions data.
    /// </summary>
    public class CcdscanTransactions
    {
        /// <summary>
        /// Page info.
        /// </summary>
        [JsonPropertyName("pageInfo")]
        public CcdscanPageInfo? PageInfo { get; set; }

        /// <summary>
        /// Nodes.
        /// </summary>
        [JsonPropertyName("nodes")]
        public IList<CcdscanTransactionNode> Nodes { get; set; } = new List<CcdscanTransactionNode>();
    }
}