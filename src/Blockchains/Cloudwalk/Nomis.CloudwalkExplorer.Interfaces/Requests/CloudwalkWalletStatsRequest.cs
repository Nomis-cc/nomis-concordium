// ------------------------------------------------------------------------------------------------------
// <copyright file="CloudwalkWalletStatsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Requests;

namespace Nomis.CloudwalkExplorer.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the wallet stats for Cloudwalk.
    /// </summary>
    public sealed class CloudwalkWalletStatsRequest :
        BaseEvmWalletStatsRequest
    {
    }
}