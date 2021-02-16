using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using System;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class HostBuilderExtentionsTests
    {
        [Fact]
        public void GetArrayStringTest()
        {
            IHostBuilder builder = new HostBuilder();

            Action action = () => builder.SetupLogger();
            action.Should().NotThrow();
        }
    }
}
