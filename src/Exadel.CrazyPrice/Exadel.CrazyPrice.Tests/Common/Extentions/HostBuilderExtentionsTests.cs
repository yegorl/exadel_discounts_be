using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class HostBuilderExtentionsTests
    {
        [Fact]
        public void SetupLoggerTest()
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("Logging", "enabled"),
                        new("LogToSerilog", "true"),
                        new("LogToNLog","true")
                    })
                    .Build();
            });

            Action action = () => builder.SetupLogger().Build();

            action.Should().NotThrow();
        }

        [Fact]
        public void SetupLoggerFailTest()
        {
            var builder = new HostBuilder().ConfigureAppConfiguration(configBuilder =>
            {
                configBuilder
                    .AddInMemoryCollection(new KeyValuePair<string, string>[]
                    {
                        new("Logging", "enabled"),
                        new("LogToSerilog", "enabled"),
                        new("LogToNLog","enabled")
                    })
                    .Build();
            });

            Action action = () => builder.SetupLogger().Build();

            action.Should().Throw<ArgumentException>();
        }
    }
}
