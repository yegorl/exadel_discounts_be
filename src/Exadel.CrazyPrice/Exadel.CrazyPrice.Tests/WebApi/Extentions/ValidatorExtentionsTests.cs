using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Exadel.CrazyPrice.Tests.WebApi.Extentions
{
    public class ValidatorExtentionsTests
    {
        private readonly IRuleBuilderInitial<Address, string> _builder;

        public ValidatorExtentionsTests()
        {
            var validator = new InlineValidator<Address>();
            _builder = validator.RuleFor(x => x.Country);
            _builder.Configure(_ => { });
        }

        [Fact]
        public void ValidCharacters()
        {
            _builder.ValidCharacters(CharOptions.Letter, "").Should().NotBeNull();
        }

        [Fact]
        public void FirstLetter()
        {
            _builder.FirstLetter().Should().NotBeNull();
        }

        [Fact]
        public void FirstDigit()
        {
            _builder.FirstDigit().Should().NotBeNull();
        }

        [Fact]
        public void Location()
        {
            _builder.Location().Should().NotBeNull();
        }

        [Fact]
        public void NotContainsSpace()
        {
            _builder.NotContainsSpace().Should().NotBeNull();
        }

        [Fact]
        public void ValidSearchDate()
        {
            _builder.ValidSearchDate().Should().NotBeNull();
        }

        [Fact]
        public void ValidWorkingDays()
        {
            _builder.ValidWorkingDays().Should().NotBeNull();
        }
    }
}
