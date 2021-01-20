using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    public interface ICrazyPriceRepository
    {        

        /// <summary>
        /// Gets all Discount Programs.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DiscountResponse>> GetAllDiscountProgramsAsync();
                
        /// <summary>
        /// Gets a Discount Program by id.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task<DiscountResponse> GetDiscountProgramAsync(string id);

        /// <summary>
        /// Gets a Discount for Moderator Program by id Discount Program.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task<UpsertDiscountRequest> GetDiscountProgramForModeratorAsync(string id);

        /// <summary>
        /// Creates or updates a Discount Program by Moderator.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateDiscountProgramAsync(UpsertDiscountRequest item);

        /// <summary>
        /// Removes Discount Program by id Discount Program.
        /// </summary>
        /// <param name="id">The id Discount Program.</param>
        /// <returns></returns>
        Task RemoveDiscountProgramAsync(string id);       
    }
}
