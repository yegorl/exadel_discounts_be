using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Validators.Special
{
    public class SpecialValidatorTests
    {
        [Theory]
        [InlineData("1", true)]
        [InlineData("-1", false)]
        [InlineData("a", false)]
        [InlineData(1, false)]
        [InlineData("", false)]
        public void FirstDigitValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).FirstDigit();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("-1", false)]
        [InlineData("a", true)]
        [InlineData(1, false)]
        [InlineData("", false)]
        public void FirstLetterValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).FirstLetter();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("-1", false)]
        [InlineData("a", false)]
        [InlineData(1, false)]
        [InlineData("", false)]
        public void LocationValidatorFalseTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).Location();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void LocationValidatorTrueTest()
        {
            var location = new Location();

            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).Location();
            var result = validator.Validate(location).IsValid;
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("1", true)]
        [InlineData("-1", true)]
        [InlineData("a", true)]
        [InlineData(1, false)]
        [InlineData("", true)]
        [InlineData(" ", false)]
        public void NotContainsSpaceValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).NotContainsSpace();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("-1", false)]
        [InlineData("a", false)]
        [InlineData(1, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        [InlineData("0101010", true)]
        [InlineData("001010", false)]
        [InlineData("00101880", false)]
        public void WorkingDaysValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).ValidWorkingDays();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("-1", false)]
        [InlineData("a", true)]
        [InlineData(1, false)]
        [InlineData("", true)]
        [InlineData(" ", false)]
        [InlineData("0101010", false)]
        [InlineData("001010", false)]
        [InlineData("00101880", false)]
        public void ValidCharactersValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).ValidCharacters(CharOptions.Letter, " ");
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1", false)]
        [InlineData("-1", false)]
        [InlineData("a", false)]
        [InlineData(1, false)]
        [InlineData("", false)]
        [InlineData(" ", false)]
        public void SearchDateTimeCriteriaValidatorTest(object value, bool expectedResult)
        {
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).ValidSearchDate();
            var result = validator.Validate(value).IsValid;
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void SearchDateTimeCriteriaValidatorTrueTest()
        {
            var value = new SearchDateCriteria()
            {
                SearchStartDate = "01.01.2020 10:10:10".GetUtcDateTime(),
                SearchEndDate = "01.01.2021 10:10:10".GetUtcDateTime()
            };
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).ValidSearchDate();
            var result = validator.Validate(value).IsValid;
            result.Should().BeTrue();
        }

        [Fact]
        public void SearchDateTimeCriteriaValidatorNullTrueTest()
        {
            var value = new SearchDateCriteria()
            {
                SearchStartDate = "01.01.2020 10:10:10".GetUtcDateTime()
            };
            AbstractValidator<object> validator = new InlineValidator<object>();
            validator.RuleFor(x => x).ValidSearchDate();
            var result = validator.Validate(value).IsValid;
            result.Should().BeTrue();
        }
    }
}
