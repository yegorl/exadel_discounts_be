using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Models;

namespace Exadel.CrazyPrice.IdentityServer.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by user e-mail
        /// </summary>
        /// <param name="email">E-mail of user</param>
        /// <returns>User or null if not found</returns>
        Task<CustomUser> GetUserByEmail(string email);

        /// <summary>
        /// Get user by Uid
        /// </summary>
        /// <param name="userUid">Uid of user</param>
        /// <returns>User or null if not found</returns>
        Task<CustomUser> GetUserByUid(string userUid);
    }
}
