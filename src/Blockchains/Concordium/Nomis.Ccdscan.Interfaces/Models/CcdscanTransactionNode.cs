// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanTransactionNode.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan account transaction node data.
    /// </summary>
    public class CcdscanTransactionNode
    {
        /// <summary>
        /// Transaction.
        /// </summary>
        [JsonPropertyName("transaction")]
        public CcdscanTransaction? Transaction { get; set; }
    }
}