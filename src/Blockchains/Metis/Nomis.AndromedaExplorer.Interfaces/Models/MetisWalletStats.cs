﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="MetisWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Stats;

namespace Nomis.AndromedaExplorer.Interfaces.Models
{
    /// <summary>
    /// Metis wallet stats.
    /// </summary>
    public sealed class MetisWalletStats :
        BaseEvmWalletStats<MetisTransactionIntervalData>
    {
        /// <inheritdoc/>
        public override string NativeToken => "METIS";
    }
}