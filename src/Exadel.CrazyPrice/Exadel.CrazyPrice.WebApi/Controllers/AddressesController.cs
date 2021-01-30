using Exadel.CrazyPrice.Common.Interfaces;
using FluentValidation.AspNetCore;
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
        /// <response code="404">No countries found.</response>
        [HttpGet, Route("get/countries/{searchCountry}"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountries([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry)
        {
            _logger.LogInformation("Country name incoming: {searchCountry}", searchCountry);
            var countries = await _repository.GetCountriesAsync(searchCountry);

            if (countries == null || countries.Count == 0)
            {
                _logger.LogWarning("Countries get: {@countries}.", countries);
                return NotFound("No countries found.");
            }

            _logger.LogInformation("Countries get: {@countries}.", countries);
            return Ok(countries);
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Countries found.</response>
        /// <response code="400">Bad request.</response> 
        /// <response code="404">No countries found.</response>
        [HttpGet, Route("get/countries"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountries()
        {
            return await GetCountries(string.Empty);
        }

        /// <summary>
        /// Gets cities from string by country.
        /// </summary>
        /// <param name="searchCountry">The country.</param>
        /// <param name="searchCity">The part name city.</param>
        /// <returns></returns>
        /// <response code="200">Cities found.</response>
        /// <response code="400">Bad request.</response> 
        /// <response code="404">No cities found.</response>
        [HttpGet, Route("get/cities/{searchCountry}/{searchCity}"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCities(
            [FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry,
            [FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCity)
        {
            _logger.LogInformation("City name incoming: {searchCity}. Country: {searchCountry}.", searchCity, searchCountry);
            var cities = await _repository.GetCitiesAsync(searchCountry, searchCity);

            if (cities == null || cities.Count == 0)
            {
                _logger.LogWarning("Cities get: {@cities}. Country: {searchCountry}.", cities, searchCountry);
                return NotFound("No cities found.");
            }

            _logger.LogInformation("Cities get: {@cities}. Country: {searchCountry}.", cities, searchCountry);
            return Ok(cities);
        }

        /// <summary>
        /// Gets all cities by country.
        /// </summary>
        /// <returns></returns>
        /// <param name="searchCountry">The country.</param>
        /// <response code="200">Cities found.</response>
        /// <response code="400">Bad request.</response> 
        /// <response code="404">No cities found.</response>
        [HttpGet, Route("get/cities/{searchCountry}"),
        ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
        ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCities([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string searchCountry)
        {
            return await GetCities(searchCountry, string.Empty);
        }
    }
}
