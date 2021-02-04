using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Models;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographicService _cryptographicService;

        public UserService(ICryptographicService cryptographicService)
        {
            _cryptographicService = cryptographicService;
        }

        public bool ValidateCredentials(CustomUser user, string password)
        {
            return _cryptographicService.ComparePasswordHash(password, user.HashPassword, user.Salt);
        }
    }
}
