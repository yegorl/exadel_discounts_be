using Exadel.CrazyPrice.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on companies.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ILogger<CompaniesController> _logger;
        private readonly ICompanyRepository _repository;

        /// <summary>
        /// Creates Company Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public CompaniesController(ILogger<CompaniesController> logger, ICompanyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets company names from string.
        /// </summary>
        /// <param name="companyName">The search string.</param>
        /// <returns></returns>
        /// <response code="200">Company names found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No company names found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/{companyName}"),
         ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetCompanyNames([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string companyName)
        {
            _logger.LogInformation("Company name incoming: {companyName}", companyName);
            var companies = await _repository.GetCompanyNamesAsync(companyName);

            if (companies == null || companies.Count == 0)
            {
                _logger.LogWarning("Companies get: {@companies}.", companies);
                return NotFound("No company names found.");
            }

            _logger.LogInformation("Companies get: {@companies}", companies);
            return Ok(companies);
        }
    }
}
