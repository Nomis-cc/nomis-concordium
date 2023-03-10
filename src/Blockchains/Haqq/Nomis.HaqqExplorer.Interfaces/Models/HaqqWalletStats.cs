// ------------------------------------------------------------------------------------------------------
// <copyright file="HaqqWalletStats.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Stats;

namespace Nomis.HaqqExplorer.Interfaces.Models
{
    /// <summary>
    /// HAQQ wallet stats.
    /// </summary>
    public sealed class HaqqWalletStats :
        BaseEvmWalletStats<HaqqTransactionIntervalData>
    {
        /// <inheritdoc/>
        public override string NativeToken => "ISLM";
    }
}