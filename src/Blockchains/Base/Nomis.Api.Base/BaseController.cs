// ------------------------------------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nomis">
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
using Nomis.Basescan.Interfaces;
using Nomis.Basescan.Interfaces.Models;
using Nomis.Basescan.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Base
{
    /// <summary>
    /// A controller to aggregate all Base-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Base Goerli blockchain.")]
    public sealed class BaseController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/base-goerli";

        /// <summary>
        /// Common tag for Base actions.
        /// </summary>
        internal const string BaseTag = "Base";

        private readonly ILogger<BaseController> _logger;
        private readonly IBaseScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="BaseController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IBaseScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public BaseController(
            IBaseScoringService scoringService,
            ILogger<BaseController> logger)
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
        ///     GET /api/v1/base-goerli/wallet/0xEAC00cB25279d5a73D82255517E738530cCb4601/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetBaseWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetBaseWalletScore",
            Tags = new[] { BaseTag })]
        [ProducesResponseType(typeof(Result<BaseWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetBaseWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] BaseWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<BaseWalletStatsRequest, BaseWalletScore, BaseWalletStats, BaseTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}