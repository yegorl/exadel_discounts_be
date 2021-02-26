using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on Addresses.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/addresses")]
    [Authorize(Roles = "Employee,Moderator,Administrator")]
    public class AddressesController : ControllerBase
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IAddressRepository _repository;

        public AddressesController(ILogger<AddressesController> logger, IAddressRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets countries from string.
        /// </summary>
        /// <param name="searchCountry">The part name country.</param>
        /// <returns></returns>
        /// <response code="200">Countries found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No countries found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/countries/{searchCountry}"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
        ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
        ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
        ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry)
        {
            var incomingUser = ControllerContext.IncomingUser();

            var countries = await _repository.GetCountriesAsync(searchCountry, searchCountry.GetLanguageFromFirstLetter());

            if (countries.IsNullOrEmpty())
            {
                _logger.LogWarning("Get countries. Country: {searchCountry}. Result is Empty. User: {@incomingUser}.", searchCountry, incomingUser);
                return NotFound("No countries found.");
            }

            _logger.LogInformation("Get countries. Country: {searchCountry}. Result: {@countries}. User: {@incomingUser}.", searchCountry, countries, incomingUser);
            return Ok(countries);
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Countries found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No countries found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("all/{language}/countries"),
         ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountriesAll([FromRoute] LanguageOption language)
        {
            var incomingUser = ControllerContext.IncomingUser();

            var countries = await _repository.GetCountriesAsync(string.Empty, language);

            if (countries.IsNullOrEmpty())
            {
                _logger.LogWarning("Get all countries. Language: {@language}. Result Empty. User: {@incomingUser}.", language, incomingUser);
                return NotFound("No countries found.");
            }

            _logger.LogInformation("Get all countries. Language: {@language}. Result: {@countries}. User: {@incomingUser}.", language, countries, incomingUser);
            return Ok(countries);
        }

        /// <summary>
        /// Gets cities from string by country.
        /// </summary>
        /// <param name="searchCountry">The country.</param>
        /// <param name="searchCity">The part name city.</param>
        /// <returns></returns>
        /// <response code="200">Cities found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No cities found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/cities/{searchCountry}/{searchCity}"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
        ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
        ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
        ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
        ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCities(
            [FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry,
            [FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCity)
        {
            var incomingUser = ControllerContext.IncomingUser();

            var cities = await _repository.GetCitiesAsync(searchCountry, searchCity, searchCity.GetLanguageFromFirstLetter());

            if (cities.IsNullOrEmpty())
            {
                _logger.LogWarning("Get Cities. Country: {searchCountry}. City: {searchCity}. Result is Empty. User: {@incomingUser}.", searchCountry, searchCity, incomingUser);
                return NotFound("No cities found.");
            }

            _logger.LogInformation("Get Cities. Country: {searchCountry}. City: {searchCity}. Result: {@cities}. User: {@incomingUser}.", searchCountry, searchCity, cities, incomingUser);
            return Ok(cities);
        }

        /// <summary>
        /// Gets all cities by country.
        /// </summary>
        /// <returns></returns>
        /// <param name="language"></param>
        /// <param name="searchCountry">The country.</param>
        /// <response code="200">Cities found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No cities found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("all/{language}/cities/{searchCountry}"),
         ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCitiesAll([FromRoute] LanguageOption language, [FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry)
        {
            var incomingUser = ControllerContext.IncomingUser();

            var cities = await _repository.GetCitiesAsync(searchCountry, string.Empty, language);

            if (cities.IsNullOrEmpty())
            {
                _logger.LogWarning("Get All Cities. Language: {@language}. Country: {searchCountry}. Result is Empty. User: {@incomingUser}.", language, searchCountry, incomingUser);
                return NotFound("No cities found.");
            }

            _logger.LogInformation("Get All Cities. Language: {@language}. Country: {searchCountry}. Result: {@cities}. User: {@incomingUser}.", language, searchCountry, cities, incomingUser);
            return Ok(cities);
        }
    }
}
