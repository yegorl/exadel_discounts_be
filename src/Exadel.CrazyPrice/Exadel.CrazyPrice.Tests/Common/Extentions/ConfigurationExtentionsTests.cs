using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class ConfigurationExtentionsTests
    {
        private readonly IConfiguration _configuration;

        public ConfigurationExtentionsTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.Development.json", true, false)
                .Build();
        }

        [Fact]
        public void GetStringTest()
        {
            var value = _configuration.GetString("Auth:IssuerUrl");
            value.Should().NotBeEmpty();
        }

        [Fact]
        public void GetArrayStringTest()
        {
            var value = _configuration.GetArrayString("Auth:Origins");
            value.Should().NotBeEmpty();
        }

        [Fact]
        public void GetUriTest()
        {
            var value = _configuration.GetUri("Auth:Swagger:AuthorizationUrl");
            value.Should().NotBeNull();
        }

        [Fact]
        public void GetDictionaryStringTest()
        {
            var value = _configuration.GetDictionaryString("Auth:Swagger:Scopes");
            value.Should().NotBeNull();
        }
    }
}
