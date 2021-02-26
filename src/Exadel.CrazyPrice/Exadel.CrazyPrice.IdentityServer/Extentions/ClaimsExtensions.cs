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
            claims.FirstOrDefault(c => c.Type == type)?.Value;
    }
}
