// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanResult.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Ccdscan.Interfaces.Models
{
    /// <summary>
    /// Ccdscan result data
    /// </summary>
    public class CcdscanResult
    {
        /// <summary>
        /// Result.
        /// </summary>
        [JsonPropertyName("__typename")]
        public string? Result { get; set; }
    }
}