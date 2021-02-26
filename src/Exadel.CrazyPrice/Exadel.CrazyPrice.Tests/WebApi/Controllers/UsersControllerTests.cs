using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly Mock<IUserRepository> _mockRepository;
        private User _resultValues;
        private readonly Guid _searchValue;
        private readonly ControllerContext _controllerContext;

        public UsersControllerTests()
        {
            _searchValue = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<UsersController>>();
            _mockRepository = new Mock<IUserRepository>();
            _controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new List<Claim>()
                            {
                                new("sub", "f7211928-669e-4d40-b9e1-35b685945a04"),
                                new("role", "employee")
                            }
                        ))
                }
            };
        }

        [Fact]
        public async Task GetUsersOkTest()
        {
            _resultValues = new User()
            {
                Id = Guid.NewGuid()
            };

            _mockRepository.Setup(r => r.GetUserByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new UsersController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetUser(_searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<Employee>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUsersNotFoundTest()
        {
            _resultValues = new User();

            _mockRepository.Setup(r => r.GetUserByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new UsersController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetUser(_searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No employee found.");
        }
    }
}
