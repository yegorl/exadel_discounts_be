using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly Mock<IUserRepository> _mockRepository;
        private User _resultValues;
        private readonly Guid _searchValue;

        public UsersControllerTests()
        {
            _searchValue = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<UsersController>>();
            _mockRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetPersonsOkTest()
        {
            _resultValues = new User();

            _mockRepository.Setup(r => r.GetUserByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new UsersController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetUser(_searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<User>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPersonsNotFoundTest()
        {
            _resultValues = null;

            _mockRepository.Setup(r => r.GetUserByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new UsersController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetUser(_searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No persons found.");
        }
    }
}
