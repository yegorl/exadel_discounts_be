using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Extentions;
using FluentAssertions;
using NSubstitute.ExceptionExtensions;
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
        [InlineData(null)]
        [InlineData("")]
        [InlineData("en")]
        [InlineData("  english")]
        [InlineData("  =")]
        public void GetLanguageFromFirstLetterEnTest(string language)
        {
            var value = language.GetLanguageFromFirstLetter();
            value.Should().BeEquivalentTo(LanguageOption.En);
        }

        [Theory]
        [InlineData("язык")]
        [InlineData("  язык")]
        public void GetLanguageFromFirstLetterRuTest(string language)
        {
            var value = language.GetLanguageFromFirstLetter();
            value.Should().BeEquivalentTo(LanguageOption.Ru);
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
