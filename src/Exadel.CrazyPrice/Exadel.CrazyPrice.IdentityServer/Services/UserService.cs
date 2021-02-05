using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.IdentityServer.Interfaces;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographicService _cryptographicService;

        public UserService(ICryptographicService cryptographicService)
        {
            _cryptographicService = cryptographicService;
        }

        public bool ValidateCredentials(User user, string password)
        {
            return _cryptographicService.ComparePasswordHash(password, user.HashPassword, user.Salt);
        }
    }
}
