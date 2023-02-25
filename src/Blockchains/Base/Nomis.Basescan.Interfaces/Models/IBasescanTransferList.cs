// ------------------------------------------------------------------------------------------------------
// <copyright file="IBasescanTransferList.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Nomis.Basescan.Interfaces.Models
{
    /// <summary>
    /// Basescan transfer list.
    /// </summary>
    /// <typeparam name="TListItem">Basescan transfer.</typeparam>
    public interface IBasescanTransferList<TListItem>
        where TListItem : IBasescanTransfer
    {
        /// <summary>
        /// List of transfers.
        /// </summary>
        [JsonPropertyName("result")]
        [DataMember(EmitDefaultValue = true)]
        public IList<TListItem>? Data { get; set; }
    }
}