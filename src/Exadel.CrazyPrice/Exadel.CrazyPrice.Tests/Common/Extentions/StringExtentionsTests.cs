using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using System;
using System.Globalization;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class StringExtentionsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData(null, "")]
        [InlineData("ReplaceSpaceTest", "ReplaceSpaceTest")]
        [InlineData(" Replace Space Test ", "Replace Space Test")]
        [InlineData("    Replace Space     Test ", "Replace Space Test")]
        [InlineData("    ReplaceSpaceTest     ", "ReplaceSpaceTest")]
        [InlineData("RemoveNonLette.,:;&$%()-+]+$rAndNonDigit/\\", "RemoveNonLetterAndNonDigit")]
        [InlineData(" Replace Space\n  \rTest\v", "Replace Space Test")]
        public void GetValidContentTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Letter, " ").Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("Магазин`чик", "Магазин`чик")]
        public void GetValidContentSpecialTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Letter | CharOptions.Symbol, " ").Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("Магазин`чик & Company", "Магазин`чик & Company")]
        public void GetValidContentLetterSymbolPunctuationTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Letter | CharOptions.Symbol | CharOptions.Punctuation, " ").Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(".,:;&$%()-+]+$", ".,:;&$%()-+]+$")]
        public void GetValidContentSymbolPunctuationTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Symbol | CharOptions.Punctuation, " ").Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("!@\"#;%:&?*()-_{}[]\\/,.", "!@\"#;%:&?*()-_{}[]\\/,.")]
        public void GetValidContentPunctuationTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Punctuation).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("`~№$^+=|<>", "`~№$^+=|<>")]
        public void GetValidContentSymbolTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Symbol).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("Aб1  9 Separator \u0020 Control \u0001", "Aб1  9 Separator \u0020 Control \u0001")]
        public void GetValidContentDifTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Upper 
                                  | CharOptions.Lower
                                  | CharOptions.Digit
                                  | CharOptions.Number
                                  | CharOptions.Separator
                                  | CharOptions.Control
                                  | CharOptions.WhiteSpace).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("This is English.", LanguageOption.En)]
        [InlineData("Это русский.", LanguageOption.Ru)]
        [InlineData("5456544+94849-+ Это русский.", LanguageOption.Ru)]
        [InlineData("This is Не русский.", LanguageOption.En)]
        [InlineData("Это not English.", LanguageOption.Ru)]
        [InlineData("2342314523*/*--*/`", LanguageOption.Unknown)]
        [InlineData("", LanguageOption.Unknown)]
        [InlineData(null, LanguageOption.Unknown)]
        public void GetLanguageFromFirstLetterTest(string value, LanguageOption expectedResult)
        {
            value.GetLanguageFromFirstLetter().Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("+375     29 852 78 94", "+375 29 852 78 94")]
        public void GetValidContentNumberTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Number | CharOptions.Punctuation | CharOptions.Symbol, " -#.").Should().Be(expectedResult);
        }

        [Fact]
        public void ReplaceTwoAndMoreCharsBySomeOneNullExceptionTest()
        {
            const string value = "";
            Action act = () => value.ReplaceTwoAndMoreCharsBySomeOne("");
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ReplaceTwoAndMoreCharsBySomeOneCharsNullExceptionTest()
        {
            const string value = "s";
            Action act = () => value.ReplaceTwoAndMoreCharsBySomeOne(new char[]{});
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReplaceTwoAndMoreCharsBySomeOneEmptyTest(string value, string expectedResult)
        {
            value.ReplaceTwoAndMoreCharsBySomeOne(new char[] { }).Should().Be(expectedResult);
        }

        [Fact]
        public void ReplaceTwoAndMoreCharsBySomeOneTrimTest()
        {
            "- -".ReplaceTwoAndMoreCharsBySomeOne(new[] {'-', ' '}).Should().Be(string.Empty);
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