using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Repositories;
using Exadel.CrazyPrice.IdentityServer.Services;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Services
{
    public class IdentityProfileServiceTests
    {
        private readonly IUserRepository _userRepository;

        public IdentityProfileServiceTests()
        {
            _userRepository = new TestUserRepository();
        }
        [Fact]
        public void GetProfileDataAsync_ShouldAddRoleToContext()
        {
            //Arrange
            var profileService = new IdentityProfileService(_userRepository);
            var dataContext = new ProfileDataRequestContext();
            var claims = new List<Claim>();
            var customClaim = new Claim("role", "some role");
            claims.Add(customClaim);
            dataContext.IssuedClaims = claims;
            //Assert
            Assert.Contains<Claim>(customClaim, dataContext.IssuedClaims);
        }
    }
}