﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="AcalaAPISettings.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Utils.Contracts.Common;

namespace Nomis.Api.Acala.Settings
{
    /// <summary>
    /// Acala API settings.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class AcalaAPISettings :
        IApiSettings
    {
        /// <inheritdoc/>
        public bool APIEnabled { get; set; }

        /// <inheritdoc/>
        public string APIName => AcalaController.AcalaTag;

        /// <inheritdoc/>
        public string ControllerName => nameof(AcalaController);
    }
}