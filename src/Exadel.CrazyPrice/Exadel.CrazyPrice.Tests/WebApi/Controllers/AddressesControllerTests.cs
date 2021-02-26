using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class AddressesControllerTests
    {
        private readonly Mock<ILogger<AddressesController>> _mockLogger;
        private readonly Mock<IAddressRepository> _mockRepository;
        private readonly List<string> _resultValues;
        private readonly ControllerContext _controllerContext;

        public AddressesControllerTests()
        {
            _resultValues = new List<string>();
            _mockLogger = new Mock<ILogger<AddressesController>>();
            _mockRepository = new Mock<IAddressRepository>();
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
        public async Task GetCountriesOkTest()
        {
            var searchValue = "bel";
            _resultValues.Add("Belarus");

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue, LanguageOption.En))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCountries(searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Belarus");
        }

        [Fact]
        public async Task GetCountriesNotFoundTest()
        {
            var searchValue = "bel";
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue, LanguageOption.Ru))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCountries(searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No countries found.");
        }

        [Fact]
        public async Task GetCitiesOkTest()
        {
            var searchCountry = "Belarus";
            var searchCity = "Mi";
            _resultValues.Add("Minsk");

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity, LanguageOption.En))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCities(searchCountry, searchCity);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Minsk");
        }

        [Fact]
        public async Task GetCitiesNotFoundTest()
        {
            var searchCountry = "bel";
            var searchCity = "Mi";
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity, LanguageOption.Ru))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCities(searchCountry, searchCity);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No cities found.");
        }

        [Fact]
        public async Task GetCitiesAllNotFoundTest()
        {
            var searchCountry = "bel";
            _resultValues.Clear();

            _mockRepository.Setup(
                    r => r.GetCitiesAsync(
                        It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LanguageOption>()))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCitiesAll(LanguageOption.Ru, searchCountry);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No cities found.");
        }

        [Fact]
        public async Task GetCitiesAllOkTest()
        {
            var searchCountry = "Belarus";
            _resultValues.Add("Minsk");

            _mockRepository.Setup(r => r.GetCitiesAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<LanguageOption>()))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCitiesAll(LanguageOption.Ru, searchCountry);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Minsk");
        }

        [Fact]
        public async Task GetCountriesAllOkTest()
        {
            _resultValues.Add("Belarus");

            _mockRepository.Setup(r => r.GetCountriesAsync(It.IsAny<string>(), It.IsAny<LanguageOption>()))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCountriesAll(LanguageOption.En);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Belarus");
        }

        [Fact]
        public async Task GetCountriesAllNotFoundTest()
        {
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCountriesAsync(It.IsAny<string>(), It.IsAny<LanguageOption>()))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetCountriesAll(LanguageOption.En);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No countries found.");
        }
    }
}
