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
using IdentityModel;

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
            _cryptographicService.ComparePasswordHash(password, user.HashPassword, user.Salt);

        public bool TryCreateUser(List<Claim> claims, ProviderOption provider, out User user)
        {
            var id = claims.GetClaimValue(ClaimTypes.NameIdentifier) ?? claims.GetClaimValue(JwtClaimTypes.Subject);
            var name = claims.GetClaimValue(ClaimTypes.GivenName);
            var surname = claims.GetClaimValue(ClaimTypes.Surname);
            var mail = claims.GetClaimValue(ClaimTypes.Email);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(mail))
            {
                var externalUser = _userRepository.GetExternalUserByEmailAsync(mail).GetAwaiter().GetResult();
                if (!externalUser.IsEmpty())
                {
                    user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Surname = surname,
                        Mail = mail,
                        Roles = externalUser.Roles,
                        ExternalUsers = new List<ExternalUser>
                        {
                            new ExternalUser
                            {
                                Identifier = id,
                                Provider = provider
                            }
                        },
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
