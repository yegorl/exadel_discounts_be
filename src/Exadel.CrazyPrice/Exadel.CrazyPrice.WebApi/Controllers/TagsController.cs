using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        /// <response code="204">No tags.</response>
        /// <response code="414">Input string is too long.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(204)]
        [ProducesResponseType(414)]
        [Route("{name}")]
        public async Task<IActionResult> GetTags(string name)
        {
            if (name.Length > 200)
            {
                return StatusCode(414);
            }

            var tags = await _repository.GetTagAsync(name);

            if (tags == null || tags.Count == 0)
            {
                return StatusCode(204);
            }

            return Ok(tags);
        }
    }
}
