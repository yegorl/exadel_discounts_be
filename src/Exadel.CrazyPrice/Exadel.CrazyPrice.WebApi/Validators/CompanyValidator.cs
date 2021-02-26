using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Company", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(200)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ,.-");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(2000)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ,.-");

                RuleFor(x => x.Mail)
                    .NotEmpty()
                    .MinimumLength(6)
                    .MaximumLength(50)
                    .EmailAddress()
                    .NotContainsSpace();
            });
        }
    }
}