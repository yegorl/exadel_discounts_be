using Exadel.CrazyPrice.Common.Interfaces;
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
            var sub = new Guid(context.Subject.GetSubjectId());
            var user = await _userRepository.GetUserByUidAsync(sub);

            var claims = new List<Claim>();
            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("role", user.Roles.ToString()));
            claims.Add(new Claim("name", user.Name));
            claims.Add(new Claim("surname", user.Surname));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = new Guid(context.Subject.GetSubjectId());
            var user = await _userRepository.GetUserByUidAsync(sub);
            context.IsActive = user != null;
        }
    }
}

