﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="BttcscanSettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Contracts;

namespace Nomis.Bttcscan.Settings
{
    /// <summary>
    /// Bttcscan settings.
    /// </summary>
    internal class BttcscanSettings :
        IBlockchainSettings
    {
        /// <summary>
        /// API key for bttcscan.
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// API base URL.
        /// </summary>
        /// <remarks>
        /// <see href="https://bttcscan.com/apis"/>
        /// </remarks>
        public string? ApiBaseUrl { get; set; }

        /// <inheritdoc />
        public BlockchainDescriptor? BlockchainDescriptor { get; set; }
    }
}