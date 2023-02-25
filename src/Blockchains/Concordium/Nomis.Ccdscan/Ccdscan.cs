// ------------------------------------------------------------------------------------------------------
// <copyright file="Ccdscan.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Nomis.Ccdscan.Extensions;
using Nomis.Ccdscan.Interfaces;

namespace Nomis.Ccdscan
{
    /// <summary>
    /// Ccdscan service registrar.
    /// </summary>
    public sealed class Ccdscan :
        IConcordiumServiceRegistrar
    {
        /// <inheritdoc/>
        public IServiceCollection RegisterService(
            IServiceCollection services)
        {
            return services
                .AddCcdscanService();
        }
    }
}