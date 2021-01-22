using System;
using System.Globalization;
using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class StringExtentionsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData(null, null)]
        [InlineData("ReplaceSpaceTest", "ReplaceSpaceTest")]
        [InlineData(" Replace Space Test ", "Replace Space Test")]
        [InlineData("    Replace Space     Test ", "Replace Space Test")]
        [InlineData("    ReplaceSpaceTest     ", "ReplaceSpaceTest")]
        public void ReplaceTwoAndMoreSpaceByOneTest(string value, string expectedResult)
        {
            value.ReplaceTwoAndMoreSpaceByOne().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("RemoveNonLette.,:;&$%()-+]+$rAndNonDigit/\\", "RemoveNonLetterAndNonDigit")]
        [InlineData(" Replace Space\n  \rTest\v", "Replace Space Test")]
        public void GetOnlyLettersDigitsAndOneSpaceTest(string value, string expectedResult)
        {
            value.GetOnlyLettersDigitsAndOneSpace().Should().Be(expectedResult);
        }

        [Fact]
        public void GetDateTimeInvariantTest()
        {
            const string value = "01.01.2010 00:00:00";
            var expectedResult =
                DateTime.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            value.GetDateTimeInvariant().Should().Be(expectedResult);
        }

        [Fact]
        public void GetDateTimeInvariantNullExceptionTest()
        {
            const string value = "";
            Action act = () => value.GetDateTimeInvariant();
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void GetDateTimeInvariantFormatExceptionTest()
        {
            const string value = "01.01.2010  50:00:00";
            Action act = () => value.GetDateTimeInvariant();
            act.Should().Throw<FormatException>();
        }

        [Theory]
        [InlineData("0101111", true)]
        [InlineData("0000100", true)]
        [InlineData("111111", false)]
        [InlineData("11 1111", false)]
        [InlineData("01010101", false)]
        [InlineData("dfg3443", false)]
        [InlineData("dfg\n43", false)]
        public void IsValidWorkingDaysTest(string value, bool expectedResult)
        {
            value.IsValidWorkingDays().Should().Be(expectedResult);
        }
    }
}