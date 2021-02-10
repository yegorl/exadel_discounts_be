using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class DiscountsControllerTests
    {
        private readonly Mock<ILogger<DiscountsController>> _mockLogger;
        private readonly Mock<IDiscountRepository> _mockRepository;

        public DiscountsControllerTests()
        {
            _mockLogger = new Mock<ILogger<DiscountsController>>();
            _mockRepository = new Mock<IDiscountRepository>();
        }

        [Fact]
        public async Task GetDiscountOkTest()
        {
            var searchValue = Guid.NewGuid();
            var resultValues = new Discount()
            {
                Id = searchValue
            };

            _mockRepository.Setup(r => r.GetDiscountByUidAsync(searchValue))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetDiscount(searchValue, LanguageOption.Ru);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<DiscountResponse>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetDiscountNotFoundTest()
        {
            var searchValue = Guid.NewGuid();
            var resultValues = (Discount)null;

            _mockRepository.Setup(r => r.GetDiscountByUidAsync(searchValue))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetDiscount(searchValue, LanguageOption.Ru);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No discount found.");
        }

        [Fact]
        public async Task GetDiscountsOkTest()
        {
            var searchValue = new SearchCriteria();
            var resultValues = new List<Discount>
            {
                new Discount()
            };

            _mockRepository.Setup(r => r.GetDiscountsAsync(searchValue))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetDiscounts(searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<DiscountResponse>>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetDiscountsNotFoundTest()
        {
            var searchValue = new SearchCriteria();
            var resultValues = (List<Discount>)null;

            _mockRepository.Setup(r => r.GetDiscountsAsync(searchValue))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetDiscounts(searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No discounts found.");
        }
        
        [Fact]
        public async Task UpsertDiscountBadRequestTest()
        {
            var searchValue = new Discount();
            var resultValues = (Discount)null;

            _mockRepository.Setup(r => r.UpsertDiscountAsync(searchValue))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.UpsertDiscount(searchValue);

            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No discounts were create or update.");
        }

        [Fact]
        public async Task DeleteDiscountOkTest()
        {
            var searchValue = Guid.NewGuid();
            var userId = Guid.Parse("0aed5f3e-3c8b-434d-adb0-e75bbca32b38");

            _mockRepository.Setup(r => r.RemoveDiscountByUidAsync(searchValue, userId));

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.DeleteDiscount(searchValue);

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task DeleteDiscountsOkTest()
        {
            var searchValue = new List<Guid>();
            var userId = Guid.Parse("0aed5f3e-3c8b-434d-adb0-e75bbca32b38");

            _mockRepository.Setup(r => r.RemoveDiscountAsync(searchValue, userId));

            var controller = new DiscountsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.DeleteDiscounts(searchValue);

            Assert.IsType<OkResult>(actionResult);
        }
    }
}
