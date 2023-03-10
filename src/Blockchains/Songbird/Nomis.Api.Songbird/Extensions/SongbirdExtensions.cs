// ------------------------------------------------------------------------------------------------------
// <copyright file="SongbirdExtensions.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Api.Common.Extensions;
using Nomis.Api.Songbird.Settings;
using Nomis.ScoringService.Interfaces.Builder;
using Nomis.SongbirdExplorer.Interfaces;

namespace Nomis.Api.Songbird.Extensions
{
    /// <summary>
    /// Songbird extension methods.
    /// </summary>
    public static class SongbirdExtensions
    {
        /// <summary>
        /// Add Songbird blockchain.
        /// </summary>
        /// <typeparam name="TServiceRegistrar">The service registrar type.</typeparam>
        /// <param name="optionsBuilder"><see cref="IScoringOptionsBuilder"/>.</param>
        /// <returns>Returns <see cref="IScoringOptionsBuilder"/>.</returns>
        public static IScoringOptionsBuilder WithSongbirdBlockchain<TServiceRegistrar>(
            this IScoringOptionsBuilder optionsBuilder)
            where TServiceRegistrar : ISongbirdServiceRegistrar, new()
        {
            return optionsBuilder
                .With<SongbirdAPISettings, TServiceRegistrar>();
        }
    }
}