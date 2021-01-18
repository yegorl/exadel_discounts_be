using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exadel.CrazyPrice.Tests
{
    public class SwaggerExtentionTests
    {
        [Fact]
        public void AddSwaggerTest()
        {
            var service = new ServiceCollection();
            var addSwagger = service.AddSwagger();
            addSwagger.Count.Should().Be(service.Count);
        }
    }
}
