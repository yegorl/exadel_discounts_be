using Exadel.CrazyPrice.Common.Models;
using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Get external user my email
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<AllowedExternalUser> GetExternalUserByEmailAsync(string mail);

        /// <summary>
        /// Get user by external id
        /// </summary>
        /// <param name="externalUser"></param>
        /// <returns></returns>
        Task<User> GetUserByExternalUserAsync(ExternalUser externalUser);

        /// <summary>
        /// Add external user into user by userUid
        /// </summary>
        /// <param name="externalUser"></param>
        /// <param name="userUid"></param>
        /// <returns></returns>
        Task AddExternalUserIntoUserAsync(ExternalUser externalUser, Guid userUid);

        /// <summary>
        /// Update user async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> UpdateUserAsync(User user);
    }
}
