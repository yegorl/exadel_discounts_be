using FluentAssertions;
using Xunit;

namespace Exadel.CrazyPrice.Common.Extentions.Tests
{
    public class StringExtentionTests
    {
        [Theory]
        [InlineData("ReplaceSpaceTest", "ReplaceSpaceTest")]
        [InlineData(" Replace Space Test ", "Replace Space Test")]
        [InlineData("    Replace Space     Test ", "Replace Space Test")]
        [InlineData("    ReplaceSpaceTest     ", "ReplaceSpaceTest")]
        public void ReplaceTwoAndMoreSpaceByOneTest(string value, string expectedResult)
        {
            value.ReplaceTwoAndMoreSpaceByOne().Should().Be(expectedResult);
        }
    }
}