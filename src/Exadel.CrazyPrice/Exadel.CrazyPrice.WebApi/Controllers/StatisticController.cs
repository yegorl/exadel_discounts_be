using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistic;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on Statistic.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/statistic")]
    [ApiController]
    public class StatisticController : Controller
    {
        private readonly ILogger<StatisticController> _logger;
        private readonly IStatisticRepository _statistic;
        public StatisticController(ILogger<StatisticController> logger, IStatisticRepository statistic)
        {
            _logger = logger;
            _statistic = statistic;
        }

        /// <summary>
        /// Gets the discounts statistic.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Got statistic.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost, Route("discounts"),
         ProducesResponseType(typeof(DiscountsStatistic), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDiscountsStatistic([FromBody] DiscountsStatisticCriteria criteria)
        {
            if (criteria.CreateEndDate < criteria.CreateStartDate)
            {
                _logger.LogWarning("Not valid request. CreateEndDate field must be greater than CreateStartDate field.");
                return BadRequest("CreateEndDate field must be greater than CreateStartDate field.");
            }
            _logger.LogInformation("DiscountsStatisticCriteria incoming: {@criteria}", criteria);
            var statistic = await _statistic.GetDiscountsStatistic(criteria);

            if (statistic == null)
            {
                _logger.LogWarning("Can't generate statistics.");
                return NotFound("Can't generate statistics for the specified criteria.");
            }

            _logger.LogInformation("Discounts statistic get: {@statistic}", statistic);
            return Ok(statistic);
        }

        /// <summary>
        /// Gets the users statistic.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Got statistic.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("users"),
         ProducesResponseType(typeof(UsersStatistic), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsersStatistic()
        {
            var statistic = await _statistic.GetUsersStatistic();
            _logger.LogInformation("Users statistic get: {@statistic}", statistic);
            return Ok(statistic);
        }
    }
}
