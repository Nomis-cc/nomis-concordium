// ------------------------------------------------------------------------------------------------------
// <copyright file="DogechainController.cs" company="Nomis">
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
using Nomis.DogechainExplorer.Interfaces;
using Nomis.DogechainExplorer.Interfaces.Models;
using Nomis.DogechainExplorer.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Dogechain
{
    /// <summary>
    /// A controller to aggregate all Dogechain-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Dogechain blockchain.")]
    public sealed class DogechainController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/dogechain";

        /// <summary>
        /// Common tag for Dogechain actions.
        /// </summary>
        internal const string DogechainTag = "Dogechain";

        private readonly ILogger<DogechainController> _logger;
        private readonly IDogechainScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="DogechainController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IDogechainScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public DogechainController(
            IDogechainScoringService scoringService,
            ILogger<DogechainController> logger)
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
        ///     GET /api/v1/dogechain/wallet/0x2A3B6a19e2aC09eaCAEB68c4F4866c0AF9194056/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetDogechainWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetDogechainWalletScore",
            Tags = new[] { DogechainTag })]
        [ProducesResponseType(typeof(Result<DogechainWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetDogechainWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] DogechainWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<DogechainWalletStatsRequest, DogechainWalletScore, DogechainWalletStats, DogechainTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}