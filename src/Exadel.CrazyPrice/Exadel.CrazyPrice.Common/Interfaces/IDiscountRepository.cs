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
        /// Gets a Discount by uid.
        /// </summary>
        /// <param name="uid">The uid Discount.</param>
        /// <returns></returns>
        Task<DiscountResponse> GetDiscountByUidAsync(Guid uid);

        /// <summary>
        /// Inserts or updates a Discount.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> UpsertDiscountAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Removes Discount by uid.
        /// </summary>
        /// <param name="uid">The uid Discount.</param>
        /// <returns></returns>
        Task RemoveDiscountByUidAsync(Guid uid);

        /// <summary>
        /// Removes Discounts by uids.
        /// </summary>
        /// <param name="uids">Discount uids.</param>
        /// <returns></returns>
        Task RemoveDiscountAsync(List<Guid> uids);
    }
}
