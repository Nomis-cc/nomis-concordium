// ------------------------------------------------------------------------------------------------------
// <copyright file="ConcordiumController.cs" company="Nomis">
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
using Nomis.Ccdscan.Interfaces;
using Nomis.Ccdscan.Interfaces.Models;
using Nomis.Ccdscan.Interfaces.Requests;
using Nomis.SoulboundTokenService.Interfaces.Enums;
using Nomis.Utils.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace Nomis.Api.Concordium
{
    /// <summary>
    /// A controller to aggregate all Concordium-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Concordium blockchain.")]
    public sealed class ConcordiumController :
        ControllerBase
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        internal const string BasePath = "api/v{version:apiVersion}/concordium";

        /// <summary>
        /// Common tag for Concordium actions.
        /// </summary>
        internal const string ConcordiumTag = "Concordium";

        private readonly ILogger<ConcordiumController> _logger;
        private readonly IConcordiumScoringService _scoringService;

        /// <summary>
        /// Initialize <see cref="ConcordiumController"/>.
        /// </summary>
        /// <param name="scoringService"><see cref="IConcordiumScoringService"/>.</param>
        /// <param name="logger"><see cref="ILogger{T}"/>.</param>
        public ConcordiumController(
            IConcordiumScoringService scoringService,
            ILogger<ConcordiumController> logger)
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
        ///     GET /api/v1/concordium/wallet/4D44RYigFqPkABrRAHXSBBQqG4VNhXEsyJrt2GH6V2H8tS1tN3/score?scoreType=0&amp;nonce=0&amp;deadline=133160867380732039
        /// </remarks>
        /// <response code="200">Returns Nomis Score and stats.</response>
        /// <response code="400">Address not valid.</response>
        /// <response code="404">No data found.</response>
        /// <response code="500">Unknown internal error.</response>
        [HttpGet("wallet/{address}/score", Name = "GetConcordiumWalletScore")]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = "GetConcordiumWalletScore",
            Tags = new[] { ConcordiumTag })]
        [ProducesResponseType(typeof(Result<ConcordiumWalletScore>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RateLimitResult), StatusCodes.Status429TooManyRequests)]
        [ProducesResponseType(typeof(ErrorResult<string>), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetConcordiumWalletScoreAsync(
            [Required(ErrorMessage = "Request should be set")] ConcordiumWalletStatsRequest request,
            CancellationToken cancellationToken = default)
        {
            switch (request.ScoreType)
            {
                case ScoreType.Finance:
                    return Ok(await _scoringService.GetWalletStatsAsync<ConcordiumWalletStatsRequest, ConcordiumWalletScore, ConcordiumWalletStats, ConcordiumTransactionIntervalData>(request, cancellationToken));
                default:
                    throw new NotImplementedException();
            }
        }
    }
}