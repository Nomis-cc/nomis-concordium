// ------------------------------------------------------------------------------------------------------
// <copyright file="AptoslabsExplorerSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.AptoslabsExplorer.Settings
{
    /// <summary>
    /// Aptoslabs Explorer settings.
    /// </summary>
    internal class AptoslabsExplorerSettings :
        IBlockchainSettings
    {
        /// <summary>
        /// Aptoslabs Explorer GraphQL API base address.
        /// </summary>
        /// <remarks>
        /// <see href="https://aptos.dev/guides/indexing/"/>
        /// </remarks>
        public string? ApiBaseUrl { get; set; }

        /// <inheritdoc />
        public BlockchainDescriptor? BlockchainDescriptor { get; set; }
    }
}