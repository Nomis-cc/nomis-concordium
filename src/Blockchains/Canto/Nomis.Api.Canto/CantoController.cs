﻿// ------------------------------------------------------------------------------------------------------
// <copyright file="CantoController.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nomis.Api.Common.Swagger.Examples;
using Nomis.CantoExplorer.Interfaces;
using Nomis.CantoExplorer.Interfaces.Models;
using Nomis.CantoExplorer.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Canto
{
    /// <summary>
    /// A controller to aggregate all Canto-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Canto blockchain.")]
    public sealed class CantoController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/canto";

        /// <summary>
        /// Common tag for Canto actions.
        /// </summary>
        internal const string CantoTag = "Canto";

        private readonly ILogger<CantoController> _logger;
        private readonly ICantoScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="CantoController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="ICantoScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public CantoController(
            ICantoScoringService scoringService,
            ILogger<CantoController> logger)
        {
            _scoringService = scoringService ?? throw new ArgumentNullException(nameof(scoringService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Nomis Score for given wallet address.
        /// </summary>
        /// <param name="request">Request for getting the wallet stats.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>An Nomis Score value and corresponding statistical data.</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/v1/canto/wallet/0xD8eaa935e527C87F19A44DED9bCBea7f4285A56c/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetCantoWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetCantoWalletScore",
            Tags = new[] { CantoTag })]
        [ProducesResponseType(typeof(Result<CantoWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetCantoWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] CantoWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<CantoWalletStatsRequest, CantoWalletScore, CantoWalletStats, CantoTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}