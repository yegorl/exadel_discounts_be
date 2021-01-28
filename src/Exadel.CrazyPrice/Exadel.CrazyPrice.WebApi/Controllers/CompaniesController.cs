using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        ///     GET /api/v1.0/companies/appl
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
        /// <response code="204">No companies.</response>
        /// <response code="414">Input string is too long.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(204)]
        [ProducesResponseType(414)]
        [Route("{companyName}")]
        public async Task<IActionResult> GetCompanyNames(string companyName)
        {
            if (companyName.Length > 200)
            {
                return StatusCode(414);
            }

            var companies = await _repository.GetCompanyAsync(companyName);

            if (companies == null || companies.Count == 0)
            {
                return StatusCode(204);
            }

            return Ok(companies);
        }
    }
}
