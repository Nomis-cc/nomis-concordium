// ------------------------------------------------------------------------------------------------------
// <copyright file="CcdscanGraphQLClient.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using Nomis.Ccdscan.Interfaces;

namespace Nomis.Ccdscan
{
    /// <inheritdoc cref="GraphQLHttpClient"/>
    // ReSharper disable once InconsistentNaming
    internal sealed class CcdscanGraphQLClient :
        GraphQLHttpClient,
        ICcdscanGraphQLClient
    {
        /// <summary>
        /// Initialize <see cref="CcdscanGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CcdscanGraphQLClient(string endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CcdscanGraphQLClient"/>.
        /// </summary>
        /// <param name="endPoint">Endpoint.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CcdscanGraphQLClient(Uri endPoint, IGraphQLWebsocketJsonSerializer serializer)
            : base(endPoint, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CcdscanGraphQLClient"/>.
        /// </summary>
        /// <param name="configure"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CcdscanGraphQLClient(Action<GraphQLHttpClientOptions> configure, IGraphQLWebsocketJsonSerializer serializer)
            : base(configure, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CcdscanGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        public CcdscanGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer)
            : base(options, serializer)
        {
        }

        /// <summary>
        /// Initialize <see cref="CcdscanGraphQLClient"/>.
        /// </summary>
        /// <param name="options"><see cref="GraphQLHttpClientOptions"/>.</param>
        /// <param name="serializer"><see cref="IGraphQLWebsocketJsonSerializer"/>.</param>
        /// <param name="httpClient"><see cref="HttpClient"/>.</param>
        public CcdscanGraphQLClient(GraphQLHttpClientOptions options, IGraphQLWebsocketJsonSerializer serializer, HttpClient httpClient)
            : base(options, serializer, httpClient)
        {
        }
    }
}