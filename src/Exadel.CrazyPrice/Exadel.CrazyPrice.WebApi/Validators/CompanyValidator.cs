using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
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
                    .Transform(d => d.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(30);

                RuleFor(x => x.Description)
                    .Transform(d => d.ReplaceTwoAndMoreSpaceByOne())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(200);
            });
        }
    }
}