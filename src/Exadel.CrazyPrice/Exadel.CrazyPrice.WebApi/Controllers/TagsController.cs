using Exadel.CrazyPrice.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1.0/tags/appl
        /// 
        /// Sample response:
        /// 
        ///     [
        ///         "apple", "application", "applause"
        ///     ]
        /// 
        /// </remarks>
        /// <param name="name">The search string.</param>
        /// <returns></returns>
        /// <response code="200">Tags found.</response>
        /// <response code="400">Bad request.</response> 
        /// <response code="404">No tags.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("get/{name}")]
        public async Task<IActionResult> GetTags([FromRoute, CustomizeValidator(RuleSet = "SearchString")] string name)
        {
            _logger.LogInformation("Tag name incoming: {name}", name);
            var tags = await _repository.GetTagAsync(name);

            if (tags == null || tags.Count == 0)
            {
                _logger.LogWarning("Tags get: {@tags}.", tags);
                return StatusCode(StatusCodes.Status404NotFound);
            }

            _logger.LogInformation("Tags get: {@tags}.", tags);
            return Ok(tags);
        }
    }
}
