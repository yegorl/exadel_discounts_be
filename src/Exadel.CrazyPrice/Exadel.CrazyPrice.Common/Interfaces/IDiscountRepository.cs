using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interfaces for IDiscountRepository.
    /// </summary>
    public interface IDiscountRepository
    {

        /// <summary>
        /// Gets Discounts by SearchCriteria.
        /// </summary>
        /// <returns></returns>
        Task<List<Discount>> GetDiscountsAsync(SearchCriteria searchCriteria);

        /// <summary>
        /// Gets a Discount by uid.
        /// </summary>
        /// <param name="uid">The uid Discount.</param>
        /// <returns></returns>
        Task<Discount> GetDiscountByUidAsync(Guid uid);

        /// <summary>
        /// Inserts or updates a Discount.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        Task<Discount> UpsertDiscountAsync(Discount discount);

        /// <summary>
        /// Removes Discount by uid.
        /// </summary>
        /// <param name="uid">The uid Discount.</param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task RemoveDiscountByUidAsync(Guid uid, Guid userUid);

        /// <summary>
        /// Removes Discounts by uids.
        /// </summary>
        /// <param name="uids">Discount uids.</param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task RemoveDiscountAsync(List<Guid> uids, Guid userUid);

        /// <summary>
        /// Sets the vote for discount. Returns false when already voted otherwise true.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task<bool> VoteDiscountAsync(int value, Guid discountUid, Guid userUid);

        /// <summary>
        /// Adds the discount in favorites.
        /// </summary>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task AddToFavoritesAsync(Guid discountUid, Guid userUid);

        /// <summary>
        /// Removes the discount from favorites.
        /// </summary>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task RemoveFromFavoritesAsync(Guid discountUid, Guid userUid);

        /// <summary>
        /// Adds the discount in subscriptions.
        /// </summary>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task<DiscountUserPromocodes> AddToSubscriptionsAsync(Guid discountUid, Guid userUid);

        /// <summary>
        /// Gets subscriptions of discount.
        /// </summary>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task<UserPromocodes> GetSubscriptionsAsync(Guid discountUid, Guid userUid);

        /// <summary>
        /// Removes the discount from subscriptions.
        /// </summary>
        /// <param name="discountUid"></param>
        /// <param name="userUid"></param>
        /// <param name="promocodeId"></param>
        /// <returns></returns>
        Task<UserPromocodes> RemoveFromSubscriptionsAsync(Guid discountUid, Guid userUid, Guid promocodeId);
    }
}
