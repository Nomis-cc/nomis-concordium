// ------------------------------------------------------------------------------------------------------
// <copyright file="KaruraController.cs" company="Nomis">
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
using Nomis.KaruraExplorer.Interfaces;
using Nomis.KaruraExplorer.Interfaces.Models;
using Nomis.KaruraExplorer.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Karura
{
    /// <summary>
    /// A controller to aggregate all Karura-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Karura EVM blockchain.")]
    public sealed class KaruraController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/karura";

        /// <summary>
        /// Common tag for Karura actions.
        /// </summary>
        internal const string KaruraTag = "Karura";

        private readonly ILogger<KaruraController> _logger;
        private readonly IKaruraScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="KaruraController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IKaruraScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public KaruraController(
            IKaruraScoringService scoringService,
            ILogger<KaruraController> logger)
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
        ///     GET /api/v1/karura/wallet/0xCe21398F606Ff276815242c5C5D2F57Bd3c58a7C/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetKaruraWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetKaruraWalletScore",
            Tags = new[] { KaruraTag })]
        [ProducesResponseType(typeof(Result<KaruraWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetKaruraWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] KaruraWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<KaruraWalletStatsRequest, KaruraWalletScore, KaruraWalletStats, KaruraTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}