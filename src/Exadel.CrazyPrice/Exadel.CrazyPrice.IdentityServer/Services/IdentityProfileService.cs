using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;


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
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.GetUserByUid(sub);

            var claims = new List<Claim>();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim("role", user.Role ?? string.Empty));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userRepository.GetUserByUid(sub);
            context.IsActive = user != null;
        }
    }
}

