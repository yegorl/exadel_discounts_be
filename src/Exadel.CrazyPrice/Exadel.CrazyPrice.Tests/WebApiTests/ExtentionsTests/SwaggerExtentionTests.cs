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
            const string nameSwaggerGenerator = "SwaggerGenerator";

            var service = new ServiceCollection();
            var addSwagger = service.AddSwagger();
            foreach (var serviceDescriptor in addSwagger)
            {
                if (serviceDescriptor?.ImplementationType?.Name == nameSwaggerGenerator)
                {
                    serviceDescriptor.ImplementationType.Name.Should().Be(nameSwaggerGenerator);
                    return;
                }
            }
            Assert.True(false, $"ServiceCollection is not contains {nameSwaggerGenerator}.");
        }
    }
}
