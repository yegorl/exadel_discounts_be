using System;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;

namespace Exadel.CrazyPrice.Common.Interfaces
{
    /// <summary>
    /// Represents interfaces for IUserRepository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a User by uid.
        /// </summary>
        /// <param name="uid">The uid User</param>
        /// <returns></returns>
        Task<User> GetUserByUidAsync(Guid uid);

        /// <summary>
        /// Get user by user e-mail
        /// </summary>
        /// <param name="mail">E-mail of user</param>
        /// <returns>User or null if not found</returns>
        Task<User> GetUserByEmailAsync(string mail);
    }
}
