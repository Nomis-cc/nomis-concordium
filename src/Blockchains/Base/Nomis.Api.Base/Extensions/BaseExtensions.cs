// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Base.Settings;
using Nomis.Api.Common.Extensions;
using Nomis.Basescan.Interfaces;
using Nomis.ScoringService.Interfaces.Builder;

namespace Nomis.Api.Base.Extensions
{
    /// <summary>
    /// Base extension methods.
    /// </summary>
    public static class BaseExtensions
    {
        /// <summary>
        /// Add Base blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithBaseBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : IBaseServiceRegistrar, new()
        {
            return optionsBuilder
                .With<BaseAPISettings, TServiceRegistrar>();
        }
    }
}