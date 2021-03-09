using Exadel.CrazyPrice.Common.Interfaces;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.Common.Models.Response;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.Services.Bus.EventBus.Events;
using Exadel.CrazyPrice.Services.Bus.IntegrationBus.IntegrationEvents;
using Exadel.CrazyPrice.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Controllers
{
    public class DiscountsControllerTests
    {
        private readonly Mock<ILogger<DiscountsController>> _mockLogger;
        private readonly Mock<IDiscountRepository> _mockDiscountRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IIntegrationEventService> _mockWebApiIntegrationEventService;
        private readonly ControllerContext _controllerContext;
        public DiscountsControllerTests()
        {
            _mockLogger = new Mock<ILogger<DiscountsController>>();
            _mockDiscountRepository = new Mock<IDiscountRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
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
            _mockWebApiIntegrationEventService = new Mock<IIntegrationEventService>();
            _mockWebApiIntegrationEventService.Setup(s => s.PublishThroughEventBusAsync(It.IsAny<IntegrationEvent>()));
        }

        [Fact]
        public async Task GetDiscountOkTest()
        {
            var searchValue = Guid.NewGuid();
            var resultValues = new Discount()
            {
                Id = searchValue,
                UsersPromocodes = new List<UserPromocodes>()
            };
            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountByUidAsync(It.IsAny<Guid>()))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(searchValue))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object, _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

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
            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountByUidAsync(It.IsAny<Guid>()))
                .ReturnsAsync(resultValues);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object, _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

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

            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountsAsync(searchValue))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.GetDiscounts(searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<DiscountResponse>>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetDiscountsNotFoundTest()
        {
            var searchValue = new SearchCriteria();
            var resultValues = new List<Discount>();

            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountsAsync(It.IsAny<SearchCriteria>()))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

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

            var user = new User();

            _mockDiscountRepository.Setup(r => r.UpsertDiscountAsync(searchValue))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.UpsertDiscount(searchValue);

            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No discounts were create or update.");
        }

        [Fact]
        public async Task UpsertDiscountOkTest()
        {
            var user = new User()
            {
                Id = Guid.Parse("f7211928-669e-4d40-b9e1-35b685945a04")
            };

            var searchValue = new Discount()
            {
                Id = Guid.Parse("0e69f357-2dab-451e-8ac2-46a6179fc119")
            };

            var resultValues = new Discount()
            {
                Id = Guid.Parse("0e69f357-2dab-451e-8ac2-46a6179fc119"),
                Language = LanguageOption.Ru,
                UserLastChangeDate = user,
                UserCreateDate = user,
                CreateDate = DateTime.UtcNow,
                LastChangeDate = DateTime.UtcNow,
                Deleted = false
            };


            var mockDiscountRepository = new Mock<IDiscountRepository>();
            mockDiscountRepository.Setup(r => r.UpsertDiscountAsync(It.IsAny<Discount>()))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(
                    r => r.GetUserByUidAsync(Guid.Parse("f7211928-669e-4d40-b9e1-35b685945a04")))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object,
                    mockDiscountRepository.Object, _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.UpsertDiscount(searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<UpsertDiscountRequest>(result.Value);
        }

        [Fact]
        public async Task DeleteDiscountOkTest()
        {
            var searchValue = Guid.NewGuid();
            var userId = Guid.Parse("0aed5f3e-3c8b-434d-adb0-e75bbca32b38");

            var user = new User();

            _mockDiscountRepository.Setup(r => r.RemoveDiscountByUidAsync(searchValue, userId));

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.DeleteDiscount(searchValue);

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task DeleteDiscountsOkTest()
        {
            var searchValue = new List<Guid>();
            var userId = Guid.Parse("0aed5f3e-3c8b-434d-adb0-e75bbca32b38");

            var user = new User();

            _mockDiscountRepository.Setup(r => r.RemoveDiscountAsync(searchValue, userId));

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.DeleteDiscounts(searchValue);

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task AddVoteOkTest()
        {
            _mockDiscountRepository.Setup(
                r => r.VoteDiscountAsync(
                    It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.AddVote(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"), 0);

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task AddVoteForbidTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.VoteDiscountAsync(
                        It.IsAny<int>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.AddVote(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"), 0);

            Assert.IsType<ForbidResult>(actionResult);
        }

        [Fact]
        public async Task AddToFavoritesTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.AddToFavoritesAsync(
                         It.IsAny<Guid>(), It.IsAny<Guid>()));

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.AddToFavorites(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task DeleteFromFavoritesTest()
        {
            _mockDiscountRepository.Setup(
                r => r.RemoveFromFavoritesAsync(
                    It.IsAny<Guid>(), It.IsAny<Guid>()));

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.DeleteFromFavorites(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task AddToSubscriptionsTest()
        {
            _mockDiscountRepository.Setup(
                r => r.AddToSubscriptionsAsync(
                    It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((DiscountUserPromocodes)null);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.AddToSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task AddToSubscriptionsSendTest()
        {
            _mockDiscountRepository.Setup(
                r => r.AddToSubscriptionsAsync(
                    It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(
                new DiscountUserPromocodes()
                {
                    CurrentPromocode = new Promocode()
                    {
                        Id = Guid.Parse("e16a46ae-b167-43c8-b99a-c472155e0eb8")
                    }
                });

            var user = new User();

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.AddToSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task DeleteFromSubscriptionsTest()
        {
            _mockDiscountRepository.Setup(
                r => r.RemoveFromSubscriptionsAsync(
                    It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
            { ControllerContext = _controllerContext };

            var actionResult = await controller.DeleteFromSubscriptions(
                Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"), Guid.Parse("a5e3e904-e926-4de1-93db-ad6400550278"));

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task ExistsVoteNotFoundTest()
        {
            _mockDiscountRepository.Setup(
                r => r.ExistsVoteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsVote(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async Task ExistsVoteFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.ExistsVoteAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsVote(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task ExistsInFavoritesNotFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.ExistsInFavoritesAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(false);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsInFavorites(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async Task ExistsInFavoritesFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.ExistsInFavoritesAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsInFavorites(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task ExistsInSubscriptionsNotFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.GetSubscriptionsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserPromocodes());

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsInSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async Task ExistsInSubscriptionsFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.GetSubscriptionsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserPromocodes()
                {
                    UserId = Guid.Parse("c43d2391-26f4-4187-a32c-529b4a5aa0ee"),
                    Promocodes = new List<Promocode>()
                    {
                        new Promocode()
                        {
                            Id = Guid.Parse("ba4a0cb5-15e3-40ba-8032-3f43216a0f3e")
                        }
                    }
                });

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.ExistsInSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task GetSubscriptionsNotFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.GetSubscriptionsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserPromocodes());

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task GetSubscriptionsFoundTest()
        {
            _mockDiscountRepository.Setup(
                    r => r.GetSubscriptionsAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(new UserPromocodes()
                {
                    UserId = Guid.Parse("c43d2391-26f4-4187-a32c-529b4a5aa0ee"),
                    Promocodes = new List<Promocode>()
                    {
                        new Promocode()
                        {
                            Id = Guid.Parse("ba4a0cb5-15e3-40ba-8032-3f43216a0f3e")
                        }
                    }
                });

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetSubscriptions(Guid.Parse("bd8f1979-71ca-4f47-9c31-bbdb8c4a9a28"));

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task GetUpsertDiscountOkTest()
        {
            var searchValue = Guid.NewGuid();
            var resultValues = new Discount()
            {
                Id = searchValue,
                UsersPromocodes = new List<UserPromocodes>()
            };
            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountByUidAsync(It.IsAny<Guid>()))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(searchValue))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object, _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetUpsertDiscount(It.IsAny<Guid>());

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<UpsertDiscountRequest>(result.Value);

            returnValue.Should().NotBeNull();
        }

        [Fact]
        public async Task GetUpsertDiscountNotFoundTest()
        {
            var searchValue = new SearchCriteria();
            var resultValues = new List<Discount>();

            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountsAsync(It.IsAny<SearchCriteria>()))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(It.IsAny<Guid>()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetUpsertDiscount(It.IsAny<Guid>());

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No discount found.");
        }

        [Fact]
        public async Task GetDiscountsUnauthorizedTest()
        {
            var searchValue = new SearchCriteria()
            {
                SearchSortFieldOption = SortFieldOption.DateCreate,
                CurrentUser = new CurrentUser()
                {
                    Role = RoleOption.Employee
                }
            };
            var resultValues = new List<Discount>
            {
                new Discount()
            };

            var user = new User();

            _mockDiscountRepository.Setup(r => r.GetDiscountsAsync(searchValue))
                .ReturnsAsync(resultValues);

            _mockUserRepository.Setup(r => r.GetUserByUidAsync(Guid.NewGuid()))
                .ReturnsAsync(user);

            var controller = new DiscountsController(_mockLogger.Object, _mockDiscountRepository.Object,
                    _mockUserRepository.Object, _mockWebApiIntegrationEventService.Object)
                { ControllerContext = _controllerContext };

            var actionResult = await controller.GetDiscounts(searchValue);

            Assert.IsType<UnauthorizedResult>(actionResult);
        }
    }
}
