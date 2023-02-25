// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.Ccdscan.Settings
{
    /// <summary>
    /// Ccdscan settings.
    /// </summary>
    internal class CcdscanSettings :
        IBlockchainSettings
    {
        /// <summary>
        /// Ccdscan GraphQL API base address.
        /// </summary>
        public string? ApiBaseUrl { get; set; }

        /// <inheritdoc />
        public BlockchainDescriptor? BlockchainDescriptor { get; set; }
    }
}