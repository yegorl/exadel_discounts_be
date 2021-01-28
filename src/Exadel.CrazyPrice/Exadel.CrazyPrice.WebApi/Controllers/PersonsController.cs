using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using FluentValidation.AspNetCore;
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
        public PersonsController(ILogger<PersonsController> logger/*, IPersonRepository repository*/)
        {
            _logger = logger;
            //_repository = repository;
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
        /// <response code="404">No persons.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [Route("get/{id}")]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            var person = await _repository.GetPersonAsync(id);

            if (person == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(person);
        }

        /// <summary>
        /// Creates or updates a person.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1.0/persons/update
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
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <response code="200">Person updated.</response>
        /// <response code="204">Person is not updated.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(204)]
        [Route("update")]
        public async Task<IActionResult> UpdatePerson([FromBody, CustomizeValidator(RuleSet = "UpsertPerson")] Person person)
        {
            var personResult = person;
            //var personResult = await _repository.UpsertPersonAsync(person);
            _logger.LogInformation("person: {@person}", personResult);

            if (personResult == null)
            {
                return StatusCode(204);
            }

            return Ok(personResult);
        }
    }
}
