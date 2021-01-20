using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface IDiscountRepository
    {

        /// <summary>
        /// Gets all Discount Programs.
        /// </summary>
        /// <returns></returns>
        Task<List<DiscountResponse>> GetAllDiscountsAsync();

        /// <summary>
        /// Gets a Discount Program by id.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task<DiscountResponse> GetDiscountAsync(string id);

        /// <summary>
        /// Upserts a Discount Program for Moderator by id Discount Program.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> UpsertDiscountAsync(string id);

        /// <summary>
        /// Upserts a Discount Program by Moderator.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpsertDiscountAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Removes Discount Program by id Discount Program.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task RemoveDiscountAsync(string id);
    }
}
