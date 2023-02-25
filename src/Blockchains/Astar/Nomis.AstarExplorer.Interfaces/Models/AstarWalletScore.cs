﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="AstarWalletScore.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.SoulboundTokenService.Interfaces.Enums;

namespace Nomis.AstarExplorer.Interfaces.Models
{
    /// <summary>
    /// Astar wallet score.
    /// </summary>
    public class AstarWalletScore :
        IWalletScore<AstarWalletStats, AstarTransactionIntervalData>
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Nomis minted Score in range of [0; 10000].
        /// </summary>
        public ushort MintedScore { get; set; }

        /// <summary>
        /// Score type.
        /// </summary>
        public ScoreType ScoreType => ScoreType.Finance;

        /// <summary>
        /// Soulbound token signature.
        /// </summary>
        public string? Signature { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public AstarWalletStats? Stats { get; set; }
    }
}