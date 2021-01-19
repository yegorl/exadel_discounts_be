using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exadel.CrazyPrice.Tests
{
    public class SwaggerExtentionTests
    {
        readonly IServiceCollection _service = new ServiceCollection();

        [Fact]
        public void AddSwaggerTest()
        {
            const string nameSwaggerGenerator = "SwaggerGenerator";

            var addSwagger = _service.AddSwagger();
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

        [Fact]
        public void UseSwaggerCrazyPriceTest()
        {
            _service.AddSwagger();
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());
            appBuilder.UseSwaggerCrazyPrice();
            try
            {
                var app = appBuilder.Build();
            }
            catch (System.InvalidOperationException ex)
            {
                if (ex.Message.Contains("Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware"))
                {
                    Assert.True(true);
                    return;
                };
            }

            Assert.True(false, $"IApplicationBuilder is not contains SwaggerUIMiddleware.");
        }
    }
}
