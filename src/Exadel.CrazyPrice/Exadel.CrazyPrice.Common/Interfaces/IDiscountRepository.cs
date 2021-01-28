using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface IDiscountRepository
    {

        /// <summary>
        /// Gets Discounts by SearchCriteria.
        /// </summary>
        /// <returns></returns>
        Task<List<DiscountResponse>> GetDiscountsAsync(SearchCriteria searchCriteria);

        /// <summary>
        /// Gets a Discount by id.
        /// </summary>
        /// <param name="id">The id Discount.</param>
        /// <returns></returns>
        Task<DiscountResponse> GetDiscountAsync(Guid id);

        /// <summary>
        /// Upserts a Discount.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Upserts Discounts.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<List<UpsertDiscountRequest>> UpsertDiscountAsync(UpsertDiscountRequest[] items);

        /// <summary>
        /// Removes Discounts by ids.
        /// </summary>
        /// <param name="ids">The id Discounts.</param>
        /// <returns></returns>
        Task RemoveDiscountAsync(Guid[] ids);
    }
}
