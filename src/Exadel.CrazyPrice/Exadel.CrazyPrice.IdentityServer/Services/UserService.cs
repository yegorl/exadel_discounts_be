using System;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.IdentityServer.Extentions;

namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class UserService : IUserService
    {
        private readonly ICryptographicService _cryptographicService;
        private readonly IUserRepository _userRepository;

        public UserService(ICryptographicService cryptographicService, IUserRepository userRepository)
        {
            _cryptographicService = cryptographicService;
            _userRepository = userRepository;
        }

        public bool ValidateCredentials(User user, string password) => 
            user.Type is UserTypeOption.Internal && 
            _cryptographicService.ComparePasswordHash(password, user.HashPassword, user.Salt);

        public bool TryCreateUser(List<Claim> claims, ProviderOptions provider, out User user)
        {
            var name = claims.GetClaimValue(ClaimTypes.GivenName);
            var surname = claims.GetClaimValue(ClaimTypes.Surname);
            var mail = claims.GetClaimValue(ClaimTypes.Email);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(mail))
            {
                var externalUser = _userRepository.GetExternalUserByEmailAsunc(mail).GetAwaiter().GetResult();
                if (!externalUser.IsEmpty())
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Surname = surname,
                        Mail = mail,
                        Roles = externalUser.Roles,
                        Type = UserTypeOption.External,
                        Provider = provider,
                        HashPassword = "",
                        Salt = "",
                        PhoneNumber = ""
                    };

                    return true;
                }
            }
            user = null;

            return false;
        }
    }
}
