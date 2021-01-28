using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;


namespace Exadel.CrazyPrice.IdentityServer.Services
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<CustomUser> _claimsFactory;
        private readonly UserManager<CustomUser> _userManager;

        public IdentityProfileService(UserManager<CustomUser> userManager, IUserClaimsPrincipalFactory<CustomUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Add custom claims in token here based on user properties or any other source
            claims.Add(new Claim(nameof(user.SubjectId), user.SubjectId ?? string.Empty));
            claims.Add(new Claim(nameof(user.Username), user.Username ?? string.Empty));
            claims.Add(new Claim(nameof(user.Email), user.Email ?? string.Empty));
            claims.Add(new Claim(nameof(user.Password), user.Password ?? string.Empty));
            claims.Add(new Claim(nameof(user.Role), user.Role ?? string.Empty));

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}

