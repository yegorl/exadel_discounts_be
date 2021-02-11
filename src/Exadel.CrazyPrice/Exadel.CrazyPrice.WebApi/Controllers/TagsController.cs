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
    /// An example controller performs operations on tags.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/tags")]
    public class TagsController : ControllerBase
    {
        private readonly ILogger<TagsController> _logger;
        private readonly ITagRepository _repository;

        public TagsController(ILogger<TagsController> logger, ITagRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets tags from string.
        /// </summary>
        /// <param name="name">The search string.</param>
        /// <returns></returns>
        /// <response code="200">Tags found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">No tags found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/{name}"),
         ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee, Moderator, Administrator")]
        public async Task<IActionResult> GetTags([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string name)
        {
            _logger.LogInformation("Tag name incoming: {name}", name);
            var tags = await _repository.GetTagAsync(name);

            if (tags == null || tags.Count == 0)
            {
                _logger.LogWarning("Tags get: {@tags}.", tags);
                return NotFound("No tags found.");
            }

            _logger.LogInformation("Tags get: {@tags}.", tags);
            return Ok(tags);
        }
    }
}
