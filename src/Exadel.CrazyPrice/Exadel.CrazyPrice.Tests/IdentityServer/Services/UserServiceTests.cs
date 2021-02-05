using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.IdentityServer;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Services;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Services
{
    public class UserServiceTests
    {
        private readonly ICryptographicService _cryptographicService;

        public UserServiceTests()
        {
            _cryptographicService = new CryptographicService();
        }
        [Fact]
        public void ValidateCredentials_True()
        {
            //Arrange
            var userService = new UserService(_cryptographicService);
            var user = CustomTestUsers.Users.FirstOrDefault(u => u.Mail == "Frank@gmail.com");
            //Act
            var result = userService.ValidateCredentials(user, "1111");
            //Assert
            Assert.True(result);
        }
        [Theory]
        [InlineData("35152")]
        [InlineData("dfg-+")]
        public void ValidateCredentials_False(string password)
        {
            //Arrange
            var userService = new UserService(_cryptographicService);
            var user = CustomTestUsers.Users.FirstOrDefault(u => u.Mail == "Frank@gmail.com");
            //Act
            var result = userService.ValidateCredentials(user, password);
            //Assert
            Assert.False(result);
        }
    }
}