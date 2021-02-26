using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Common.Models.Statistics;
using Exadel.CrazyPrice.WebApi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on Statistic.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/statistics")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private readonly ILogger<StatisticsController> _logger;
        private readonly IStatisticsRepository _statistics;
        public StatisticsController(ILogger<StatisticsController> logger, IStatisticsRepository statistics)
        {
            _logger = logger;
            _statistics = statistics;
        }

        /// <summary>
        /// Gets the discounts statistics.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Got statistics.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost, Route("discounts"),
         ProducesResponseType(typeof(DiscountsStatistics), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDiscountsStatistics([FromBody] DiscountsStatisticsCriteria criteria)
        {
            var incomingUser = ControllerContext.IncomingUser();

            if (criteria.CreateEndDate < criteria.CreateStartDate)
            {
                _logger.LogWarning("Get Discounts Statistics. Criteria: {@criteria}. Result is not enabled, user does not have permission. User: {@incomingUser}.", criteria, incomingUser);
                return BadRequest("CreateEndDate field must be greater than CreateStartDate field.");
            }

            var statistics = await _statistics.GetDiscountsStatistics(criteria);

            if (statistics == null)
            {
                _logger.LogWarning("Get Discounts Statistics. Criteria: {@criteria}. Result is Empty. User: {@incomingUser}.", criteria, incomingUser);
                return NotFound("Can't generate statistics for the specified criteria.");
            }

            _logger.LogInformation("Get Discounts Statistics. Criteria: {@criteria}. Result: {@statistics}. User: {@incomingUser}.", criteria, statistics, incomingUser);
            return Ok(statistics);
        }

        /// <summary>
        /// Gets the users statistics.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Got statistics.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("users"),
         ProducesResponseType(typeof(UsersStatistics), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsersStatistics()
        {
            var incomingUser = ControllerContext.IncomingUser();

            var statistics = await _statistics.GetUsersStatistics();
            _logger.LogInformation("Get Users Statistics. Result: {@statistics}. User: {@incomingUser}.", statistics, incomingUser);
            return Ok(statistics);
        }
    }
}
