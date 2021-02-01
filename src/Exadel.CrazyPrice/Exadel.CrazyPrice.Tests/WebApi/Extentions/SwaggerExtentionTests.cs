using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class SwaggerExtentionTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public SwaggerExtentionTests()
        {
            _service.AddSwagger();
        }

        [Fact]
        public void AddSwaggerTest()
        {
            var nameSwaggerGenerator = "SwaggerGenerator";
            var hasSwagger = _service.Any(serviceDescriptor => serviceDescriptor?.ImplementationType?.Name == nameSwaggerGenerator);

            hasSwagger.Should().BeTrue($"ServiceCollection is not contains {nameSwaggerGenerator}.");
        }

        [Fact]
        public void UseSwaggerCrazyPriceTest()
        {
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());
            appBuilder.UseSwaggerCrazyPrice();

            Action act = () => appBuilder.Build();

            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Unable to resolve service for type 'Microsoft.AspNetCore.Hosting.IWebHostEnvironment' while attempting to activate 'Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware'.", $"IApplicationBuilder is not contains SwaggerUIMiddleware.");
        }
    }
}
