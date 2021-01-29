using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation.AspNetCore;
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
    public class DiscountController : ControllerBase
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IDiscountRepository _repository;

        /// <summary>
        /// Creates Discount Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        public DiscountController(ILogger<DiscountController> logger, IDiscountRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets a discount by id.
        /// </summary>
        /// <param name="id">The search id of discount.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No discounts found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(DiscountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Route("get/{id}")]
        public async Task<IActionResult> GetDiscount(Guid id)
        {
            _logger.LogInformation("Guid incoming: {@id}", id);
            var discount = await _repository.GetDiscountByUidAsync(id);

            if (discount == null)
            {
                _logger.LogWarning("Discount get: {@discount}", discount);
                return NotFound("No discounts found.");
            }

            _logger.LogInformation("Discount get: {@discount}", discount);
            return Ok(discount);
        }

        /// <summary>
        /// Gets discounts by search criteria.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">No discounts found.</response>
        [HttpPost]
        [ProducesResponseType(typeof(List<DiscountResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Route("search")]
        public async Task<IActionResult> GetDiscounts([FromBody, CustomizeValidator(RuleSet = "SearchCriteria")] SearchCriteria searchCriteria)
        {
            _logger.LogInformation("SearchCriteria incoming: {@searchCriteria}", searchCriteria);
            var discounts = await _repository.GetDiscountsAsync(searchCriteria);

            if (discounts == null || discounts.Count == 0)
            {
                _logger.LogWarning("Discounts get: {@discounts}", discounts);
                return NotFound("No discounts found.");
            }

            _logger.LogInformation("Discounts get: {@discounts}", discounts);
            return Ok(discounts);
        }

        /// <summary>
        /// Creates or updates a discount.
        /// </summary>
        /// <param name="discount">The discount.</param>
        /// <returns></returns>
        /// <response code="200">Discount updated.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UpsertDiscountRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("upsert")]
        public async Task<IActionResult> UpsertDiscount([FromBody, CustomizeValidator(RuleSet = "UpsertDiscount")] UpsertDiscountRequest discount)
        {
            _logger.LogInformation("UpsertDiscountRequest incoming: {@discount}", discount);
            var upsertDiscount = await _repository.UpsertDiscountAsync(discount);

            if (upsertDiscount == null)
            {
                _logger.LogWarning("Discount upsert: {@upsertDiscount}", upsertDiscount);
                return BadRequest("No discounts were created or updated.");
            }

            _logger.LogInformation("Discount upsert: {@upsertDiscount}", upsertDiscount);
            return Ok(upsertDiscount);
        }

        /// <summary>
        /// Removes Discount by id.
        /// </summary>
        /// <param name="id">Discount id.</param>
        /// <returns></returns>
        /// <response code="200">Discounts deleted.</response>
        /// <response code="400">Bad request.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            await _repository.RemoveDiscountByUidAsync(id);
            _logger.LogInformation("Discounts deleted: {@id}", id);

            return Ok(id);
        }

        /// <summary>
        /// Removes Discounts by ids.
        /// </summary>
        /// <param name="ids">Discounts ids.</param>
        /// <returns></returns>
        /// <response code="200">Discounts deleted.</response>
        /// <response code="400">Bad request.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("deletemany")]
        public async Task<IActionResult> DeleteDiscounts([FromBody]List<Guid> ids)
        {
            await _repository.RemoveDiscountAsync(ids);
            _logger.LogInformation("Discounts deleted: {@ids}", ids);

            return Ok();
        }
    }
}
