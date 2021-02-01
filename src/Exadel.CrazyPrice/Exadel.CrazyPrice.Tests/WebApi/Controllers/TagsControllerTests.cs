﻿using Exadel.CrazyPrice.Common.Interfaces;
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
    public class TagsControllerTests
    {
        private readonly Mock<ILogger<TagsController>> _mockLogger;
        private readonly Mock<ITagRepository> _mockRepository;
        private readonly List<string> _resultValues;
        private readonly string _searchValue;

        public TagsControllerTests()
        {
            _searchValue = "mir";
            _resultValues = new List<string>();
            _mockLogger = new Mock<ILogger<TagsController>>();
            _mockRepository = new Mock<ITagRepository>();
        }

        [Fact]
        public async Task GetTagsOkTest()
        {
            _resultValues.Add("exmir");

            _mockRepository.Setup(r => r.GetTagAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new TagsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetTags(_searchValue);

            var result = Assert.IsType<OkObjectResult>(actionResult);
            var returnValue = Assert.IsType<List<string>>(result.Value);

            returnValue[0].Should().BeEquivalentTo("exmir");
        }

        [Fact]
        public async Task GetTagsNotFoundTest()
        {
            _resultValues.Clear();

            _mockRepository.Setup(r => r.GetTagAsync(_searchValue))
                .ReturnsAsync(_resultValues);

            var controller = new TagsController(_mockLogger.Object, _mockRepository.Object);

            var actionResult = await controller.GetTags(_searchValue);

            var result = Assert.IsType<NotFoundObjectResult>(actionResult);
            var returnValue = Assert.IsType<string>(result.Value);

            returnValue.Should().BeEquivalentTo("No tags found.");
        }
    }
}
