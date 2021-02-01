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
    public class AddressesControllerTests
    {
        private readonly Mock<ILogger<AddressesController>> _mockLogger;
        private readonly Mock<IAddressRepository> _mockRepository;
        private readonly List<string> _resultValues;

        public AddressesControllerTests()
        {
            _resultValues = new List<string>();
            _mockLogger = new Mock<ILogger<AddressesController>>();
            _mockRepository = new Mock<IAddressRepository>();
        }

        [Fact]
        public async Task GetCountriesOkTest()
        {
            var searchValue = "bel";
            _resultValues.Add("Belarus");

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCountries(searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Belarus");
        }

        [Fact]
        public async Task GetCountriesEmptyOkTest()
        {
            var searchValue = string.Empty;
            _resultValues.Add("Belarus");

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCountries();

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Belarus");
        }

        [Fact]
        public async Task GetCountriesNotFoundTest()
        {
            var searchValue = "bel";
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCountries(searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No countries found.");
        }

        [Fact]
        public async Task GetCountriesNullNotFoundTest()
        {
            var searchValue = string.Empty;
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCountriesAsync(searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCountries();

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

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCities(searchCountry, searchCity);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("Minsk");
        }

        [Fact]
        public async Task GetCitiesNullOkTest()
        {
            var searchCountry = "Belarus";
            var searchCity = string.Empty;
            _resultValues.Add("Minsk");

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCities(searchCountry);

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

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCities(searchCountry, searchCity);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No cities found.");
        }

        [Fact]
        public async Task GetCitiesNullNotFoundTest()
        {
            var searchCountry = "bel";
            var searchCity = string.Empty;
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetCitiesAsync(searchCountry, searchCity))
                .ReturnsAsync(_resultValues);

            var controller = new AddressesController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetCities(searchCountry);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No cities found.");
        }
    }
}
