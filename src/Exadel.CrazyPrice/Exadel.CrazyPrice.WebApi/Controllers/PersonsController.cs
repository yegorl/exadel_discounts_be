using Exadel.CrazyPrice.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on persons.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger<PersonsController> _logger;
        private readonly IPersonRepository _repository;

        /// <summary>
        /// Creates Person Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public PersonsController(ILogger<PersonsController> logger, IPersonRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets a person by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1.0/persons/get/50e97451-6c81-42b9-8d31-3e74b2ea1040
        /// 
        /// Sample response:
        /// 
        ///     {
        ///         "id": "50e97451-6c81-42b9-8d31-3e74b2ea1040",
        ///         "name": "Sam",
        ///         "surname": "Vorington",
        ///         "phoneNumber": "+375 29 852 78 94",
        ///         "mail": "sam.v@mail.com"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">The search id of person.</param>
        /// <returns></returns>
        /// <response code="200">Persons found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No persons.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("get/{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);
            var person = await _repository.GetPersonByUidAsync(id);

            if (person == null)
            {
                _logger.LogWarning("Person get: {@person}", person);
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation("Person get: {@person}", person);
            return Ok(person);
        }
    }
}
