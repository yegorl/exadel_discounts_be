using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.IdentityServer.Extentions
{
    public static class ClaimsExtensions
    {
        public static string GetClaimValue(this IEnumerable<Claim> claims, string type) =>
            claims.FirstOrDefault(c => c.Type == type).Value;

        public static User TryCreateUserFromClaims(this List<Claim> claims, ProviderOptions provider)
        {
            var name = claims.GetClaimValue(ClaimTypes.GivenName);
            var surname = claims.GetClaimValue(ClaimTypes.Surname);
            var mail = claims.GetClaimValue(ClaimTypes.Email);

            if (name == null || surname == null || mail == null) return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                Mail = mail,
                Roles = RoleOption.Employee,
                Type = UserTypeOption.External,
                Provider = provider,
                HashPassword = "",
                Salt = "",
                PhoneNumber = ""
            };

            return user;
        }
    }
}
