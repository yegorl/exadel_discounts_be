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
    public class PersonsControllerTests
    {
        private readonly Mock<ILogger<PersonsController>> _mockLogger;
        private readonly Mock<IPersonRepository> _mockRepository;
        private Person _resultValues;
        private readonly Guid _searchValue;

        public PersonsControllerTests()
        {
            _searchValue = Guid.NewGuid();
            _mockLogger = new Mock<ILogger<PersonsController>>();
            _mockRepository = new Mock<IPersonRepository>();
        }

        [Fact]
        public async Task GetPersonsOkTest()
        {
            _resultValues = new Person();

            _mockRepository.Setup(r => r.GetPersonByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new PersonsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetPerson(_searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<Person>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPersonsNotFoundTest()
        {
            _resultValues = null;

            _mockRepository.Setup(r => r.GetPersonByUidAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new PersonsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetPerson(_searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No persons found.");
        }
    }
}
