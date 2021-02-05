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
    /// An example controller performs operations on persons.
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
        /// Gets a user by id.
        /// </summary>
        /// <param name="id">The search id of user.</param>
        /// <returns></returns>
        /// <response code="200">User found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No user found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Route("get/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);
            var user = await _repository.GetUserByUidAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User get: {@user}", user);
                return NotFound("No persons found.");
            }

            _logger.LogInformation("User get: {@user}", user);
            return Ok(user);
        }
    }
}
