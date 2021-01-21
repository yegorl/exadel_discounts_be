using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
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
    }
}