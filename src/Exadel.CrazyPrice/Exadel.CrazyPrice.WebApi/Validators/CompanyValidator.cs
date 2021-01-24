using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
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
                    .Transform(d => d.GetValidContent(CharOptions.Letter, " -"))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Description)
                    .Transform(d => d.GetValidContent(CharOptions.Letter | CharOptions.Number | CharOptions.Punctuation | CharOptions.Symbol, " "))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(200);
            });
        }
    }
}