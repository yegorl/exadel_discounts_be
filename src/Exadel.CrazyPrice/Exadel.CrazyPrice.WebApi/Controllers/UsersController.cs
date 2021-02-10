using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on users.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _repository;

        /// <summary>
        /// Creates User Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public UsersController(ILogger<UsersController> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets an employee by id.
        /// </summary>
        /// <param name="id">The search id of employee.</param>
        /// <returns></returns>
        /// <response code="200">Employee found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No employee found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/{id}"),
         ProducesResponseType(typeof(Employee), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);

            var user = await _repository.GetUserByUidAsync(id);
            var employee = user.ToEmployee();

            if (employee.IsEmpty())
            {
                _logger.LogWarning("Employee get: Empty.");
                return NotFound("No employee found.");
            }

            _logger.LogInformation("Employee get: {@employee}", employee);
            return Ok(employee);
        }
    }
}
