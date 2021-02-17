using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Exadel.CrazyPrice.WebApi.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class SwaggerExtentionsTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public SwaggerExtentionsTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Auth:Swagger:OAuthClientId", "OAuthClientId"),
                    new("Auth:Swagger:OAuthAppName","OAuthAppName")
                })
                .Build();
            _service.AddSingleton(_ => configuration);
            _service.TryAddSingleton<IWebApiConfiguration, WebApiConfiguration>();
            _service.AddSwagger();
        }

        [Fact]
        public void AddSwaggerTest()
        {
            var serviceName = "SwaggerGenerator";
            var hasSwagger = _service.Any(serviceDescriptor => serviceDescriptor?.ImplementationType?.Name == serviceName);

            hasSwagger.Should().BeTrue($"ServiceCollection is not contains {serviceName}.");
        }

        [Fact]
        public void UseSwaggerCrazyPriceTest()
        {
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());
            Action act = () => appBuilder.UseSwaggerCrazyPrice().Build();

            // The SwaggerUIMiddleware exists.
            act.Should().Throw<InvalidOperationException>()
            .WithMessage("Unable to resolve service for type 'Microsoft.AspNetCore.Hosting.IWebHostEnvironment' while attempting to activate 'Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware'.");
        }
    }
}
