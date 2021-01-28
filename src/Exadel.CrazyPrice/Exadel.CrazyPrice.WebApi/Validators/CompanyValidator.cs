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
                    .MaximumLength(30)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(200)
                    .FirstLetter()
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ");
            });
        }
    }
}