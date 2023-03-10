// ------------------------------------------------------------------------------------------------------
// <copyright file="ShidenExplorer.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.ShidenExplorer.Extensions;
using Nomis.ShidenExplorer.Interfaces;

namespace Nomis.ShidenExplorer
{
    /// <summary>
    /// Shiden Explorer service registrar.
    /// </summary>
    public sealed class ShidenExplorer :
        IShidenServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddShidenExplorerService();
        }
    }
}