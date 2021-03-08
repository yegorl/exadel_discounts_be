using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Data.Extentions;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Events;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
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
        private readonly IIntegrationEventService _integrationEventService;

        /// <summary>
        /// Creates Discount Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="discounts"></param>
        /// <param name="users"></param>
        /// <param name="integrationEventService"></param>
        public DiscountsController(ILogger<DiscountsController> logger, IDiscountRepository discounts, IUserRepository users, IIntegrationEventService integrationEventService)
        {
            _logger = logger;
            _discounts = discounts;
            _users = users;
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
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
            var currentUser = ControllerContext.CurrentUser();
            var discount = (await _discounts.GetDiscountByUidAsync(id)).TransformUsersPromocodes(currentUser);

            if (discount.IsEmpty())
            {
                _logger.LogWarning("Get Discount. Guid: {@id}. Language: {@language}. Result is Empty.  User: {@currentUser}.", id, language, currentUser);
                return NotFound("No discount found.");
            }

            var response = discount.Translate(language).ToDiscountResponse();

            _logger.LogInformation("Get Discount. Guid: {@id}. Language: {@language}. Result: {@response}.  User: {@currentUser}.", id, language, response, currentUser);
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
            var currentUser = ControllerContext.CurrentUser();
            searchCriteria.CurrentUser = currentUser;

            if (searchCriteria.IsNotAdministratorSortByDateCreate(currentUser.Role))
            {
                _logger.LogWarning("Get Discounts. SearchCriteria: {@searchCriteria}. Result is Empty, user does not have permission. User: {@currentUser}.", searchCriteria, currentUser);
                return Unauthorized();
            }

            var discounts = (await _discounts.GetDiscountsAsync(searchCriteria)).TransformUsersPromocodes(currentUser);

            if (discounts == null || discounts.Count == 0)
            {
                _logger.LogWarning("Get Discounts. SearchCriteria: {@searchCriteria}. Result is Empty. User: {@currentUser}.", searchCriteria, currentUser);
                return NotFound("No discounts found.");
            }

            var discountsResponse = discounts.ToListDiscountResponse(searchCriteria.SearchLanguage);

            _logger.LogInformation("Get Discounts. SearchCriteria: {@searchCriteria}. Result: {@discountsResponse}. User: {@currentUser}.", searchCriteria, discountsResponse, currentUser);
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
            var currentUser = ControllerContext.CurrentUser();
            var discount = (await _discounts.GetDiscountByUidAsync(id));

            if (discount.IsEmpty())
            {
                _logger.LogWarning("Get Upsert Discount. Guid: {@id}. Result is Empty. User: {@currentUser}.", id, currentUser);
                return NotFound("No discount found.");
            }

            var response = discount.ToUpsertDiscountRequest();

            _logger.LogInformation("Get Upsert Discount. Guid: {@id}. Result: {@response}. User: {@currentUser}.", id, response, currentUser);
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
            var currentUser = ControllerContext.CurrentUser();
            var user = (await _users.GetUserByUidAsync(currentUser.Id)).ToUserLikeEmployee();

            var discount = upsertDiscountRequest.ToDiscount().AddChangeUserTime(user);

            var responseDiscount = await _discounts.UpsertDiscountAsync(discount);

            if (responseDiscount.IsEmpty())
            {
                _logger.LogWarning("Upsert Discount. UpsertDiscountRequest: {@upsertDiscountRequest}. Result is Empty. User: {@currentUser}.", upsertDiscountRequest, currentUser);
                return BadRequest("No discounts were create or update.");
            }

            var response = responseDiscount.ToUpsertDiscountRequest();

            _logger.LogInformation("Upsert Discount. UpsertDiscountRequest: {@upsertDiscountRequest}. Result: {@response}. User: {@currentUser}.", upsertDiscountRequest, response, currentUser);
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
            var currentUser = ControllerContext.CurrentUser();
            await _discounts.RemoveDiscountByUidAsync(id, currentUser.Id);
            _logger.LogInformation("Delete Discount. Guid: {@id}. Result: deleted. User: {@currentUser}.", id, currentUser);

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
            var currentUser = ControllerContext.CurrentUser();
            await _discounts.RemoveDiscountAsync(ids, currentUser.Id);
            _logger.LogInformation("Delete Discounts. Guid[]: {@ids}. Result: deleted. User: {@currentUser}", ids, currentUser);

            return Ok();
        }

        /// <summary>
        /// Checks the discount in favorites.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount in favorites.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("favorites/exists/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> ExistsInFavorites([FromRoute] Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();
            var exists = await _discounts.ExistsInFavoritesAsync(id, currentUser.Id);

            if (!exists)
            {
                _logger.LogInformation("Exists In Favorites. Guid: {@id}. Result: Not exists. User: {@currentUser}.", id, currentUser);
                return NotFound("Discount not exists in Favorites.");
            }

            _logger.LogInformation("Exists In Favorites. Guid: {@id}. Result: Exists. User: {@currentUser}.", id, currentUser);

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
            var currentUser = ControllerContext.CurrentUser();
            await _discounts.AddToFavoritesAsync(id, currentUser.Id);
            _logger.LogInformation("Add To Favorites. Guid: {@id}. Result: added. User: {@currentUser}.", id, currentUser);

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
            var currentUser = ControllerContext.CurrentUser();
            await _discounts.RemoveFromFavoritesAsync(id, currentUser.Id);
            _logger.LogInformation("Delete From Favorites. Guid: {@id}. Result: deleted. User: {@currentUser}.", id, currentUser);

            return Ok();
        }

        /// <summary>
        /// Gets the subscriptions of discount.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Found the subscriptions of discount.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet, Route("subscriptions/get/{id}"),
         ProducesResponseType(typeof(UserPromocodes), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> GetSubscriptions([FromRoute] Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();
            var userPromocodes = await _discounts.GetSubscriptionsAsync(id, currentUser.Id);

            if (userPromocodes.IsEmpty())
            {
                _logger.LogWarning("Get Subscriptions. Guid: {@id}. Result is Empty. User: {@currentUser}.", id, currentUser);
                return BadRequest("Not found the subscriptions of discount.");
            }

            _logger.LogInformation("Gets subscriptions. Guid: {@id}. Result: {@userPromocodes}. User: {@currentUser}.", id, userPromocodes, currentUser);

            return Ok(userPromocodes);
        }

        /// <summary>
        /// Checks the discount in subscriptions.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount in subscriptions.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("subscriptions/exists/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> ExistsInSubscriptions([FromRoute] Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();
            var userPromocodes = await _discounts.GetSubscriptionsAsync(id, currentUser.Id);

            if (userPromocodes.IsEmpty())
            {
                _logger.LogInformation("Exists In Subscriptions. Guid: {@id}. Result: Not exists. User: {@currentUser}.", id, currentUser);
                return NotFound("Discount not exists in Subscriptions.");
            }

            _logger.LogInformation("Exists In Subscriptions. Guid: {@id}. Result: Exists. User: {@currentUser}.", id, currentUser);

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
         ProducesResponseType(typeof(UserPromocodes), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> AddToSubscriptions([FromRoute] Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();
            var discountUserPromocodes = await _discounts.AddToSubscriptionsAsync(id, currentUser.Id);
            _logger.LogInformation("Add To Subscriptions. Guid: {@id}. Result: {@userPromocodes}. User: {@currentUser}.", id, discountUserPromocodes, currentUser);

            if (discountUserPromocodes != null && !discountUserPromocodes.CurrentPromocode.IsEmpty())
            {
                var employee = (await _users.GetUserByUidAsync(currentUser.Id)).ToEmployee();

                var userEvent = new PromocodeAddedIntegrationEvent<UserMailContent>
                    (new UserMailContent(employee, id, discountUserPromocodes.DiscountName,
                    discountUserPromocodes.CurrentPromocode.PromocodeValue),
                    "WebApi", new BusParams("crazyprice.direct", "mail.user"));

                await PublishThroughEventBusAsync(userEvent);

                var companyEvent = new PromocodeAddedIntegrationEvent<CompanyMailContent>
                    (new CompanyMailContent(discountUserPromocodes.Company, id, discountUserPromocodes.DiscountName,
                    discountUserPromocodes.CurrentPromocode.PromocodeValue),
                    "WebApi", new BusParams("crazyprice.direct", "mail.company"));

                await PublishThroughEventBusAsync(companyEvent);
            }

            return Ok(discountUserPromocodes?.UserPromocodes);
        }

        private async Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            await _integrationEventService.PublishThroughEventBusAsync(@event);
        }

        /// <summary>
        /// Removes the subscription from discount.
        /// </summary>
        /// <param name="discountId"></param>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        /// <response code="200">The subscription removed from discount.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("subscriptions/delete/{discountId}/{promocodeId}"),
         ProducesResponseType(typeof(UserPromocodes), StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> DeleteFromSubscriptions([FromRoute] Guid discountId, [FromRoute] Guid promocodeId)
        {
            var currentUser = ControllerContext.CurrentUser();
            var userPromocodes = await _discounts.RemoveFromSubscriptionsAsync(discountId, currentUser.Id, promocodeId);
            _logger.LogInformation("Delete From Subscriptions. Guid: {@discountId}. Promocode id: {@promocodeId}. Result: {@userPromocodes}. User: {@currentUser}.", discountId, promocodeId, userPromocodes, currentUser);

            return Ok(userPromocodes);
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
            var currentUser = ControllerContext.CurrentUser();
            var result = await _discounts.VoteDiscountAsync(value, id, currentUser.Id);
            if (!result)
            {
                _logger.LogWarning("Add Vote. Guid: {@id}. Vote: {@value}. Result is Empty, user already voted. User id: {@currentUser}.", id, value, currentUser);
                return Forbid();
            }

            _logger.LogInformation("Add Vote. Guid: {@id}. Vote: {@value}. Result: user voted. User: {@currentUser}.", id, value, currentUser);

            return Ok();
        }

        /// <summary>
        /// Checks the discount is voted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">The discount is voted.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not voted.</response>
        /// <response code="405">Method not allowed.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut, Route("vote/exists/{id}"),
         ProducesResponseType(StatusCodes.Status200OK),
         ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest),
         ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized),
         ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden),
         ProducesResponseType(typeof(string), StatusCodes.Status404NotFound),
         ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed),
         ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Employee,Moderator,Administrator")]
        public async Task<IActionResult> ExistsVote([FromRoute] Guid id)
        {
            var currentUser = ControllerContext.CurrentUser();
            var exists = await _discounts.ExistsVoteAsync(id, currentUser.Id);

            if (!exists)
            {
                _logger.LogInformation("Exists Vote. Guid: {@id}. Result: Not voted. User: {@currentUser}.", id, currentUser);
                return NotFound("Discount not voted.");
            }

            _logger.LogInformation("Exists Vote. Guid: {@id}. Result: Voted. User: {@currentUser}.", id, currentUser);

            return Ok();
        }
    }
}
