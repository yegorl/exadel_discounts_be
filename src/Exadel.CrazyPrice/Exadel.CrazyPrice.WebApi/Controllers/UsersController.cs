using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.WebApi.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Request;
using FluentValidation.AspNetCore;

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
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Already voted.</response>
        /// <response code="404">No employee found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/{id}"),
         ProducesResponseType(typeof(Employee), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();

            var user = await _repository.GetUserByUidAsync(id);
            var employee = user.ToEmployee();

            if (employee.IsEmpty())
            {
                _logger.LogWarning("Get User. Guid: {@id}. Result is Empty. User: {@currentUser}.", id, currentUser);
                return NotFound("No employee found.");
            }

            _logger.LogInformation("Get User. Guid: {@id}. Result: {@employee}. User: {@currentUser}.", id, employee, currentUser);
            return Ok(employee);
        }

        /// <summary>
        /// Gets a myself user.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Employee found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Already voted.</response>
        /// <response code="404">No employee found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get"),
         ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = ControllerContext.CurrentUser();

            var user = await _repository.GetUserByUidAsync(currentUser.Id);
            var employee = user.ToUserInfo();

            if (employee.IsEmpty())
            {
                _logger.LogWarning("Get Myself. Result is Empty. User: {@currentUser}.", currentUser);
                return NotFound("No employee found.");
            }

            _logger.LogInformation("Get Myself. Result: {@employee}. User: {@currentUser}.", employee, currentUser);
            return Ok(employee);
        }

        /// <summary>
        /// Updates UserInfo.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">UserInfo found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Already voted.</response>
        /// <response code="404">No UserInfo found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost, Route("update"),
         ProducesResponseType(typeof(UserInfo), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")] 
        public async Task<IActionResult> UpdateMyUserInfo([FromBody, CustomizeValidator(RuleSet = "UpdateUser")] UpdateUserRequest request)
        {
            var currentUser = ControllerContext.CurrentUser();

            var updatedUser = (await _repository.GetUserByUidAsync(currentUser.Id)).UpdateUser(request);

            var updatedUserInfo = (await _repository.UpdateUserAsync(updatedUser)).ToUserInfo();

            _logger.LogInformation("Update UserInfo. Result: {@userInfo} User: {@currentUser}.", updatedUserInfo, currentUser);
            return Ok(updatedUserInfo);
        }
    }
}
