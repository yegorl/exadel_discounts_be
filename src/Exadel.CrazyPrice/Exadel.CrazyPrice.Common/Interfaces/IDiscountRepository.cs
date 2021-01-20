using System;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;

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
        /// Upserts a Discount Program for Moderator by id Discount Program.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Removes Discount Program by id Discount Program.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task RemoveDiscountAsync(Guid id);
    }
}
