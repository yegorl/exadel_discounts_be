using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class CompaniesControllerTests
    {
        private readonly Mock<ILogger<CompaniesController>> _mockLogger;
        private readonly Mock<ICompanyRepository> _mockRepository;
        private readonly List<string> _resultValues;
        private readonly string _searchValue;

        public CompaniesControllerTests()
        {
            _searchValue = "comp";
            _resultValues = new List<string>();
            _mockLogger = new Mock<ILogger<CompaniesController>>();
            _mockRepository = new Mock<ICompanyRepository>();
        }

        [Fact]
        public async Task GetCompanyNamesOkTest()
        {
            _resultValues.Add("Company");

            _mockRepository.Setup(r => r.GetCompanyNamesAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new CompaniesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCompanyNames(_searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Company");
        }

        [Fact]
        public async Task GetCompanyNamesNotFoundTest()
        {
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCompanyNamesAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new CompaniesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCompanyNames(_searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No company names found.");
        }
    }
}
