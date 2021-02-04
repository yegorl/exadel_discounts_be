using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Models;

namespace Exadel.CrazyPrice.IdentityServer.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Validate credentials by checking email and password
        /// </summary>
        /// <param name="email">E-mail of user</param>
        /// <param name="password">Password of user</param>
        /// <returns></returns>
        bool ValidateCredentials(CustomUser user, string password);
    }
}
