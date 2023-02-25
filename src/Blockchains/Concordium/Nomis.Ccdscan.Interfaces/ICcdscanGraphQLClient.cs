// ------------------------------------------------------------------------------------------------------
// <copyright file="ICcdscanGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions;

namespace Nomis.Ccdscan.Interfaces
{
    /// <summary>
    /// GraphQL client for interaction with Ccdscan API.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ICcdscanGraphQLClient :
        IGraphQLClient
    {
    }
}