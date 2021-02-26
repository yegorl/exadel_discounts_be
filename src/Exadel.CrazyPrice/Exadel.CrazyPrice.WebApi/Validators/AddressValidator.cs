using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Address", () =>
            {
                RuleFor(x => x.Country)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.City)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.Street)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(300)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ,.-");

                RuleFor(x => x.Location)
                    .Location();
            });
        }
    }
}