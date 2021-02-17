using System;
using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class ConfigurationExtentionsTests
    {
        private readonly IConfiguration _configuration;

        public ConfigurationExtentionsTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new("Auth:IssuerUrl","IssuerUrl"),
                    new("Auth:Origins:0", "\"https://localhost\""),
                    new("Auth:Swagger:AuthorizationUrl", "http://localhost"),
                    new("Auth:Swagger:Scopes:Scope1","Scope1")
                })
                .Build();
        }

        [Fact]
        public void GetStringTest()
        {
            var value = _configuration.GetString("Auth:IssuerUrl");
            value.Should().NotBeEmpty();
        }

        [Fact]
        public void GetStringRaiseExceptionTest()
        {
            Action action = () => _configuration.GetString("Auth:Nothing");
            action.Should().Throw<ArgumentException>();
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
