using Exadel.CrazyPrice.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
        /// Gets companies from string.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1.0/companies/get/appl
        /// 
        /// Sample response:
        /// 
        ///     [
        ///         "Apple", "Conapple", "Applause"
        ///     ]
        /// 
        /// </remarks>
        /// <param name="companyName">The search string.</param>
        /// <returns></returns>
        /// <response code="200">Companies found.</response>
        /// <response code="400">Bad request.</response> 
        /// <response code="404">No companies.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("get/{companyName}")]
        public async Task<IActionResult> GetCompanyNames([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string companyName)
        {
            _logger.LogInformation("Company name incoming: {companyName}", companyName);
            var companies = await _repository.GetCompanyAsync(companyName);

            if (companies == null || companies.Count == 0)
            {
                _logger.LogWarning("Companies get: {@companies}.", companies);
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation("Companies get: {@companies}", companies);
            return Ok(companies);
        }
    }
}
