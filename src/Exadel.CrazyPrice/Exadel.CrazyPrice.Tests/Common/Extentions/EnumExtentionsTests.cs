using System;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class EnumExtentionsTests
    {
        [Theory]
        [InlineData("$k : $v", "All : 0, Favorites : 1, Reservations : 2")]
        [InlineData("$k : v", "All : v, Favorites : v, Reservations : v")]
        [InlineData("k : $v", "k : 0, k : 1, k : 2")]
        public void GetStringTest(string value, string expectedResult)
        {
            var discountOptionString = EnumExtentions.GetStringFrom<DiscountOption>(value);
            discountOptionString.Should().Be(expectedResult);
        }

        [Fact]
        public void GetStringExceptionNullTest()
        {
            Action act = ()=> EnumExtentions.GetStringFrom<DiscountOption>(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetStringFormatExceptionTest()
        {
            Action act = () => EnumExtentions.GetStringFrom<DiscountOption>("kv or nothing");
            act.Should().Throw<FormatException>();
        }
    }
}
