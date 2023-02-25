// ------------------------------------------------------------------------------------------------------
// <copyright file="ConcordiumExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Concordium.Settings;
using Nomis.Ccdscan.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Concordium.Extensions
{
    /// <summary>
    /// Concordium extension methods.
    /// </summary>
    public static class ConcordiumExtensions
    {
        /// <summary>
        /// Add Concordium blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithConcordiumBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IConcordiumServiceRegistrar, new()
        {
            return optionsBuilder
                .With<ConcordiumAPISettings, TServiceRegistrar>();
        }
    }
}