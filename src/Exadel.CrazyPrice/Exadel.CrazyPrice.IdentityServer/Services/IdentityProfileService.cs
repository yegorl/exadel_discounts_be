using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public IdentityProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await GetUserAsync(context.Subject.GetSubjectId());

            // Add custom claims in token here based on user properties or any other source
            var claims = new List<Claim>
            {
                new("role", user.Roles.ToString()),
                new("name", user.Name),
            };

            var surname = user.Surname.ToStringWithValue("Surname", string.Empty,  false);
            if (!surname.IsNullOrEmpty())
            {
                claims.Add(new Claim("surname", surname));
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await GetUserAsync(context.Subject.GetSubjectId());
            context.IsActive = !user.IsEmpty();
        }

        private async Task<User> GetUserAsync(string sub) =>
            await _userRepository.GetUserByUidAsync(sub.ToGuid(Guid.Empty, false));
    }
}

