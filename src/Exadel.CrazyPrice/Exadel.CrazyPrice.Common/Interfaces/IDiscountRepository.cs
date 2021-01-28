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
        /// Gets Discount Programs by SearchCriteria.
        /// </summary>
        /// <returns></returns>
        Task<List<DiscountResponse>> GetDiscountsAsync(SearchCriteria searchCriteria);

        /// <summary>
        /// Gets a Discount Program by id.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task<DiscountResponse> GetDiscountAsync(Guid id);

        /// <summary>
        /// Upserts a Discount Program.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Upserts a Discount Programs.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<UpsertDiscountRequest[]> UpsertDiscountAsync(UpsertDiscountRequest[] items);

        /// <summary>
        /// Removes Discount Program by ids Discount Program.
        /// </summary>
        /// <param name="ids">The id Discount Program.</param>
        /// <returns></returns>
        Task RemoveDiscountAsync(Guid[] ids);
    }
}
