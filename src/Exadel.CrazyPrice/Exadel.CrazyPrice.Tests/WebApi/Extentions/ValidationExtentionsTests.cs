using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class ValidationExtentionsTests
    {
        private readonly IServiceCollection _service = new ServiceCollection();

        public ValidationExtentionsTests()
        {
            _service.AddMvc().AddCrazyPriceValidation();
        }

        [Fact]
        public void AddCrazyPriceValidationTest()
        {
            var serviceName = "ServiceProviderValidatorFactory";
            var hasService = _service.Any(serviceDescriptor => serviceDescriptor?.ImplementationType?.Name == serviceName);

            hasService.Should().BeTrue($"ServiceCollection is not contains {serviceName}.");
        }

        [Fact]
        public void UseCrazyPriceValidationTest()
        {
            var appBuilder = new ApplicationBuilder(_service.BuildServiceProvider());

            var value = ValidatorOptions.Global.LanguageManager.Enabled;
            value.Should().BeTrue();

            appBuilder.UseCrazyPriceValidation().Build();

            value = ValidatorOptions.Global.LanguageManager.Enabled;
            value.Should().BeFalse();
        }
    }
}
