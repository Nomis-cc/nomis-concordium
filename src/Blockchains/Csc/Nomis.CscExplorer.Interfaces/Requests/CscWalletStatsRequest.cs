﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="CscWalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Requests;

namespace Nomis.CscExplorer.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the wallet stats for CSC.
    /// </summary>
    public sealed class CscWalletStatsRequest :
        BaseEvmWalletStatsRequest
    {
    }
}