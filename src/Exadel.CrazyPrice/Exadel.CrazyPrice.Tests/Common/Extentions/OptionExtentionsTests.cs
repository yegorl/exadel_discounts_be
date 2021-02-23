using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentAssertions;
using System;
using Xunit;

namespace Exadel.CrazyPrice.Tests.Common.Extentions
{
    public class OptionExtentionsTests
    {
        [Fact]
        public void ToStringLookupLanguageOptionFullTest()
        {
            var value = LanguageOption.En.ToStringLookup();
            value.Should().BeEquivalentTo("english");
        }

        [Fact]
        public void ToStringLookupLanguageOptionShortTest()
        {
            var value = LanguageOption.En.ToStringLookup(false);
            value.Should().BeEquivalentTo("en");
        }

        [Fact]
        public void ToStringLookupSortFieldOptionFullTest()
        {
            var value = SortFieldOption.NameDiscount.ToStringLookup();
            value.Should().BeEquivalentTo("name");
        }

        [Fact]
        public void ToStringLookupSortFieldOptionShortTest()
        {
            var value = SortFieldOption.NameDiscount.ToStringLookup(false);
            value.Should().BeEquivalentTo("namediscount");
        }

        [Theory]
        [InlineData("En")]
        [InlineData("en")]
        [InlineData("english")]
        public void ToLanguageOptionTest(string language)
        {
            var value = language.ToLanguageOption();
            value.Should().BeEquivalentTo(LanguageOption.En);
        }

        [Fact]
        public void ToLanguageOptionRaiseExceptionTest()
        {
            Action action = () => "default".ToLanguageOption(LanguageOption.Ru, true);
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(null, LanguageOption.En)]
        [InlineData("", LanguageOption.En)]
        [InlineData("en", LanguageOption.En)]
        [InlineData("  english", LanguageOption.En)]
        [InlineData("  =", LanguageOption.En)]
        [InlineData("язык", LanguageOption.Ru)]
        [InlineData("  язык", LanguageOption.Ru)]
        public void GetLanguageFromFirstLetterEnTest(string language, LanguageOption languageOption)
        {
            var value = language.GetLanguageFromFirstLetter();
            value.Should().BeEquivalentTo(languageOption);
        }

        [Fact]
        public void GetWithTranslationsPrefixEnTrValueTest()
        {
            var value = "language".GetWithTranslationsPrefix(LanguageOption.En);
            value.Should().BeEquivalentTo("translations.language");
        }

        [Fact]
        public void GetWithTranslationsPrefixEnValueTest()
        {
            var value = "language".GetWithTranslationsPrefix(LanguageOption.En, true);
            value.Should().BeEquivalentTo("translations.");
        }

        [Fact]
        public void GetWithTranslationsPrefixRuTrValueTest()
        {
            var value = "language".GetWithTranslationsPrefix(LanguageOption.Ru);
            value.Should().BeEquivalentTo("language");
        }

        [Fact]
        public void GetWithTranslationsPrefixRuValueTest()
        {
            var value = "language".GetWithTranslationsPrefix(LanguageOption.Ru, true);
            value.Should().BeEquivalentTo("");
        }
    }
}
