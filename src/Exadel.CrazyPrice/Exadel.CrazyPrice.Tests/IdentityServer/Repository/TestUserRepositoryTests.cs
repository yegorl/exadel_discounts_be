using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.IdentityServer.Interfaces;
using Exadel.CrazyPrice.IdentityServer.Repositories;
using Xunit;

namespace Exadel.CrazyPrice.Tests.IdentityServer.Repository
{
    public class TestUserRepositoryTests
    {
        private readonly IUserRepository _userRepository;

        public TestUserRepositoryTests()
        {
            _userRepository = new TestUserRepository();
        }

        [Fact]
        public async void GetUserByEmail_ShouldWork()
        {
            //Arrange 
            var email = "Frank@gmail.com";
            //Acr
            var result = await _userRepository.GetUserByEmailAsync(email);
            //Assert
            Assert.IsType<User>(result);
            Assert.True(result.Mail == email);
        }
        [Fact]
        public async void GetUserByEmail_ShouldNotWork()
        {
            //Arrange 
            var email = "NotExistUser@gmail.com";
            //Acr
            var result = await _userRepository.GetUserByEmailAsync(email);
            //Assert
            Assert.Null(result);
        }
    }
}