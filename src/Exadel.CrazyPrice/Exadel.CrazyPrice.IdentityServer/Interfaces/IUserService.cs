using Exadel.CrazyPrice.Common.Models;

namespace Exadel.CrazyPrice.IdentityServer.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Validate credentials by checking email and password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password">Password of user</param>
        /// <returns></returns>
        bool ValidateCredentials(User user, string password);
    }
}
