using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1.0/discounts/get/50e97451-6c81-42b9-8d31-3e74b2ea1040
        /// 
        /// </remarks>
        /// <param name="id">The search id of discount.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="404">No discounts.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [Route("get/{id}")]
        public async Task<IActionResult> GetDiscount(Guid id)
        {
            var discount = await _repository.GetDiscountAsync(id);

            if (discount == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(discount);
        }

        /// <summary>
        /// Gets discounts by search criteria.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1.0/discounts/search
        /// 
        /// </remarks>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns></returns>
        /// <response code="200">Discounts found.</response>
        /// <response code="404">No discounts.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [Route("search")]
        public async Task<IActionResult> GetDiscounts([FromBody, CustomizeValidator(RuleSet = "SearchCriteria")] SearchCriteria searchCriteria)
        {
            var discounts = await _repository.GetDiscountsAsync(searchCriteria);

            if (discounts == null || discounts.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(discounts);
        }

        /// <summary>
        /// Creates or updates a discount.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1.0/discounts/update
        /// 
        /// Sample response:
        /// 
        /// </remarks>
        /// <param name="discount">The discount.</param>
        /// <returns></returns>
        /// <response code="200">Discount updated.</response>
        /// <response code="500">Discount is not updated.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        [Route("update")]
        public async Task<IActionResult> UpdateDiscount([FromBody, CustomizeValidator(RuleSet = "UpsertDiscount")] UpsertDiscountRequest discount)
        {
            var upsertDiscount = await _repository.UpsertDiscountAsync(discount);

            if (upsertDiscount == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(upsertDiscount);
        }

        /// <summary>
        /// Creates or updates discounts.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1.0/discounts/update
        /// 
        /// </remarks>
        /// <param name="discounts">Discounts array.</param>
        /// <returns></returns>
        /// <response code="200">Discounts updated.</response>
        /// <response code="500">Discounts is not updated.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        [Route("updatemany")]
        public async Task<IActionResult> UpdateManyDiscounts(UpsertDiscountRequest[] discounts)
        {
            var upsertDiscounts = await _repository.UpsertDiscountAsync(discounts);

            if (upsertDiscounts == null || upsertDiscounts.Count == 0)
            {
                return StatusCode(500);
            }

            return Ok(upsertDiscounts);
        }

        /// <summary>
        /// Removes Discounts by ids.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/v1.0/discounts/delete 
        /// 
        /// </remarks>
        /// <param name="ids">Discounts ids.</param>
        /// <returns></returns>
        /// <response code="200">Discounts deleted.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("delete")]
        public async Task<IActionResult> DeleteDiscounts(Guid[] ids)
        {
            await _repository.RemoveDiscountAsync(ids);

            return Ok();
        }
    }
}
