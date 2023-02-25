// ------------------------------------------------------------------------------------------------------
// <copyright file="Basescan.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Basescan.Extensions;
using Nomis.Basescan.Interfaces;

namespace Nomis.Basescan
{
    /// <summary>
    /// Basescan Explorer service registrar.
    /// </summary>
    public sealed class Basescan :
        IBaseServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddBasescanService();
        }
    }
}