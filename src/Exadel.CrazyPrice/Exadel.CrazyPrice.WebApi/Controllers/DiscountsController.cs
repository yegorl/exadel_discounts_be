using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.WebApi.Controllers
{
    /// <summary>
    /// An example controller performs operations on Discounts.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/discounts")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly ILogger<DiscountsController> _logger;
        private readonly IDiscountRepository _discounts;
        private readonly IUserRepository _users;

        /// <summary>
        /// Creates Discount Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="discounts"></param>
        /// <param name="users"></param>
        public DiscountsController(ILogger<DiscountsController> logger, IDiscountRepository discounts, IUserRepository users)
        {
            _logger = logger;
            _discounts = discounts;
            _users = users;
        }

        /// <summary>
        /// Gets the discount by id.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="id">The search id of discount.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No discount found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("get/{language}/{id}"),
         ProducesResponseType(typeof(DiscountResponse), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetDiscount([FromRoute] Guid id, [FromRoute] LanguageOption language)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);
            var discount = await _discounts.GetDiscountByUidAsync(id);

            if (discount.IsEmpty())
            {
                _logger.LogWarning("Discount is Empty.");
                return NotFound("No discount found.");
            }

            var response = discount.Translate(language).ToDiscountResponse();

            _logger.LogInformation("Discount get: {@response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Gets discounts by search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No discounts found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost, Route("search"),
         ProducesResponseType(typeof(List<DiscountResponse>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetDiscounts([FromBody, CustomizeValidator(RuleSet = "SearchCriteria")] SearchCriteria searchCriteria)
        {
            searchCriteria.SearchUserId = ControllerContext.GetUserId();

            _logger.LogInformation("SearchCriteria incoming: {@searchCriteria}", searchCriteria);
            var discounts = await _discounts.GetDiscountsAsync(searchCriteria);

            if (discounts == null || discounts.Count == 0)
            {
                _logger.LogWarning("Discounts are Empty.");
                return NotFound("No discounts found.");
            }

            var discountsResponse = discounts.ToListDiscountResponse(searchCriteria.SearchLanguage);

            _logger.LogInformation("Discounts get: {@discountsResponse}", discountsResponse);
            return Ok(discountsResponse);
        }

        /// <summary>
        /// Gets the discount by id.
        /// </summary>
        /// <param name="id">The search id of discount.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">No discount found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("upsert/get/{id}"),
         ProducesResponseType(typeof(UpsertDiscountRequest), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Moderator,Administrator")]
        public async Task<IActionResult> GetUpsertDiscount([FromRoute] Guid id)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);
            var discount = await _discounts.GetDiscountByUidAsync(id);

            if (discount.IsEmpty())
            {
                _logger.LogWarning("Discount is Empty.");
                return NotFound("No discount found.");
            }

            var response = discount.ToUpsertDiscountRequest();

            _logger.LogInformation("Discount get: {@response}", response);
            return Ok(response);
        }

        /// <summary>
        /// Creates or updates the discount.
        /// </summary>
        /// <param name="upsertDiscountRequest"></param>
        /// <returns></returns>
        /// <response code="200">Discount updated.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost, Route("upsert"),
         ProducesResponseType(typeof(UpsertDiscountRequest), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Moderator,Administrator")]
        public async Task<IActionResult> UpsertDiscount([FromBody, CustomizeValidator(RuleSet = "UpsertDiscount")] UpsertDiscountRequest upsertDiscountRequest)
        {
            var userUid = ControllerContext.GetUserId();
            var user = (await _users.GetUserByUidAsync(userUid)).ToUserLikeEmployee();

            _logger.LogInformation("UpsertDiscountRequest incoming: {@upsertDiscountRequest}, from User: {@user}", upsertDiscountRequest, user);

            var discount = upsertDiscountRequest.ToDiscount().AddChangeUserTime(user);

            var responseDiscount = await _discounts.UpsertDiscountAsync(discount);

            if (responseDiscount.IsEmpty())
            {
                _logger.LogWarning("Discount upsert is Empty.");
                return BadRequest("No discounts were create or update.");
            }

            var response = responseDiscount.ToUpsertDiscountRequest();

            _logger.LogInformation("Discount upsert: {@response}. User id: {@userUid}", response, userUid);
            return Ok(response);
        }

        /// <summary>
        /// Removes the Discount by id.
        /// </summary>
        /// <param name="id">Discount id.</param>
        /// <returns></returns>
        /// <response code="200">Discounts deleted.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete, Route("delete/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Moderator,Administrator")]
        public async Task<IActionResult> DeleteDiscount([FromRoute] Guid id)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.RemoveDiscountByUidAsync(id, userUid);
            _logger.LogInformation("Discounts deleted: {@id}. User id: {@userUid}", id, userUid);

            return Ok();
        }

        /// <summary>
        /// Removes many Discounts by ids.
        /// </summary>
        /// <param name="ids">Discounts ids.</param>
        /// <returns></returns>
        /// <response code="200">Discounts deleted.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete, Route("delete"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Moderator,Administrator")]
        public async Task<IActionResult> DeleteDiscounts([FromBody] List<Guid> ids)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.RemoveDiscountAsync(ids, userUid);
            _logger.LogInformation("Discounts deleted: {@ids}. User id: {@userUid}", ids, userUid);

            return Ok();
        }

        /// <summary>
        /// Adds the discount in favorites.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount added in favorites.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("favorites/add/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> AddToFavorites([FromRoute] Guid id)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.AddToFavoritesAsync(id, userUid);
            _logger.LogInformation("Added to favorites. Discount id: {@id}. User id: {@userUid}.", id, userUid);

            return Ok();
        }

        /// <summary>
        /// Removes the discounts from favorites.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount removed from favorites.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("favorites/delete/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> DeleteFromFavorites([FromRoute] Guid id)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.RemoveFromFavoritesAsync(id, userUid);
            _logger.LogInformation("Deleted from favorites. Discount id: {@id}. User id: {@userUid}.", id, userUid);

            return Ok();
        }

        /// <summary>
        /// Adds the discount in subscriptions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount added in subscriptions.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("subscriptions/add/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> AddToSubscriptions([FromRoute] Guid id)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.AddToSubscriptionsAsync(id, userUid);
            _logger.LogInformation("Added to subscriptions. Discount id: {@id}. User id: {@userUid}.", id, userUid);

            return Ok();
        }

        /// <summary>
        /// Removes the discount from subscriptions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount removed from subscriptions.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("subscriptions/delete/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> DeleteFromSubscriptions([FromRoute] Guid id)
        {
            var userUid = ControllerContext.GetUserId();

            await _discounts.RemoveFromSubscriptionsAsync(id, userUid);
            _logger.LogInformation("Removed from subscriptions. Discount id: {@id}. User id: {@userUid}.", id, userUid);

            return Ok();
        }

        /// <summary>
        /// Sets the vote for discount.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="200">Vote added.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Already voted.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("vote/{id}/{value}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> AddVote([FromRoute] Guid id, [FromRoute, CustomizeValidator(RuleSet = "VoteValue")] int value)
        {
            var userUid = ControllerContext.GetUserId();

            var result = await _discounts.VoteDiscountAsync(value, id, userUid);
            if (!result)
            {
                _logger.LogWarning("User already voted. Discount id: {@id}. User id: {@userUid}. Vote: {@value}.", id, userUid, value);
                return Forbid();
            }

            _logger.LogInformation("User voted. Discount id: {@id}. User id: {@userUid}. Vote: {@value}.", id, userUid, value);

            return Ok();
        }
    }
}
