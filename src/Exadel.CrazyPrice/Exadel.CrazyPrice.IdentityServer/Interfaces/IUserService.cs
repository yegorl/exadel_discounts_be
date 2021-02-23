using System.Collections.Generic;
using System.Security.Claims;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;

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
