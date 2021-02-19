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
        [InlineData("Магазин чик", "Магазинчик")]
        public void GetValidContentNospaceTest(string value, string expectedResult)
        {
            value.GetValidContent(CharOptions.Letter).Should().Be(expectedResult);
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
            Action act = () => value.ReplaceTwoAndMoreCharsBySomeOne(Array.Empty<char>());
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ReplaceTwoAndMoreCharsBySomeOneEmptyTest(string value, string expectedResult)
        {
            value.ReplaceTwoAndMoreCharsBySomeOne(Array.Empty<char>()).Should().Be(expectedResult);
        }

        [Fact]
        public void ReplaceTwoAndMoreCharsBySomeOneTrimTest()
        {
            "- -".ReplaceTwoAndMoreCharsBySomeOne(new[] { '-', ' ' }).Should().Be(string.Empty);
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

        [Theory]
        [InlineData("true", false, false, true)]
        [InlineData("true", true, false, true)]
        [InlineData("true", false, true, true)]
        [InlineData("true", true, true, true)]
        [InlineData("not", true, false, true)]
        public void ToBoolTrueReturnsTrueTest(string value, bool defaultValue, bool raiseException, bool expectedResult)
        {
            value.ToBool(defaultValue, raiseException).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("false", false, false, false)]
        [InlineData("false", true, false, false)]
        [InlineData("false", false, true, false)]
        [InlineData("false", true, true, false)]
        [InlineData("not", false, false, false)]
        public void ToBoolFalseReturnsFalseTest(string value, bool defaultValue, bool raiseException, bool expectedResult)
        {
            value.ToBool(defaultValue, raiseException).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("not", false, true)]
        [InlineData("not", true, true)]
        public void ToBoolRaiseExceptionsTest(string value, bool defaultValue, bool raiseException)
        {
            Action action = () => value.ToBool(defaultValue, raiseException);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("1", 0, false, 1)]
        [InlineData("2", 1, false, 2)]
        [InlineData("-3", 1, false, 1)]
        public void ToUintReturnsTrueTest(string value, uint defaultValue, bool raiseException, uint expectedResult)
        {
            value.ToUint(defaultValue, raiseException).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("-1", 0, true)]
        [InlineData("a", 1, true)]
        [InlineData("2,5", 1, true)]
        [InlineData("2.5", 1, true)]
        public void ToUintRaiseExceptionsTest(string value, uint defaultValue, bool raiseException)
        {
            Action action = () => value.ToUint(defaultValue, raiseException);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("1", "", false, "1")]
        [InlineData("a", "", false, "a")]
        [InlineData("n", "", false, "n")]
        [InlineData("", "default", false, "default")]
        public void ToStringWithValueReturnsValueTest(string value, string defaultValue, bool raiseException, string expectedResult)
        {
            value.ToStringWithValue(defaultValue, raiseException).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("", "default", true)]
        [InlineData(null, "default", true)]
        public void ToStringWithValueRaiseExceptionsTest(string value, string defaultValue, bool raiseException)
        {
            Action action = () => value.ToStringWithValue(defaultValue, raiseException);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("7cbe9cb7-c1be-48eb-8757-524f1b1b1e20", "49f3bbc2-6737-4002-ad77-bfd33d331940", false, "7cbe9cb7-c1be-48eb-8757-524f1b1b1e20")]
        [InlineData("", "49f3bbc2-6737-4002-ad77-bfd33d331940", false, "49f3bbc2-6737-4002-ad77-bfd33d331940")]
        public void ToGuidReturnsValueTest(string value, Guid defaultValue, bool raiseException, Guid expectedResult)
        {
            value.ToGuid(defaultValue, raiseException).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("", "49f3bbc2-6737-4002-ad77-bfd33d331940", true)]
        [InlineData("notGuid", "49f3bbc2-6737-4002-ad77-bfd33d331940", true)]
        public void ToGuidRaiseExceptionsTest(string value, Guid defaultValue, bool raiseException)
        {
            Action action = () => value.ToGuid(defaultValue, raiseException);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("url\\", true)]
        [InlineData("http/", true)]
        [InlineData("any", false)]
        public void IsLastTest(string value, bool expectedResult)
        {
            var symbols = new[] { "\\", "/" };
            value.IsLast(StringComparison.Ordinal, symbols).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("url\\", true)]
        [InlineData("http/", true)]
        [InlineData("any", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        public void IsNullOrEmptyOrLastTest(string value, bool expectedResult)
        {
            var symbols = new[] { "\\", "/" };
            value.IsNullOrEmptyOrLast(StringComparison.Ordinal, symbols).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("url\\", false)]
        [InlineData("http/", false)]
        [InlineData("any", false)]
        [InlineData(null, true)]
        [InlineData("", true)]
        public void IsNullOrEmptyTest(string value, bool expectedResult)
        {
            value.IsNullOrEmpty().Should().Be(expectedResult);
        }

        [Fact]
        public void IsNullOrEmptyStringNullTest()
        {
            string value = null;
            value.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmptyStringTypeNullTest()
        {
            var value = (string)null;
            value.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void IsNullOrEmptyArrayBeFalseTest()
        {
            var values = new[] { "", null, "s" };
            values.IsNullOrEmpty().Should().BeFalse();
        }

        [Fact]
        public void IsNullOrEmptyArrayBeTrueTest()
        {
            var values = new string[] { };
            values.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void ToUriNullTest()
        {
            string value = null;
            value.ToUri(UriKind.Absolute, false).Should().BeNull();
        }

        [Fact]
        public void ToUriOkTest()
        {
            string value = "http://localhost";
            value.ToUri().Should().BeEquivalentTo(new Uri("http://localhost", UriKind.Absolute));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("///")]
        public void ToUriExceptionTest(string value)
        {
            Action action = () => value.ToUri();
            action.Should().Throw<Exception>();
        }

    }
}