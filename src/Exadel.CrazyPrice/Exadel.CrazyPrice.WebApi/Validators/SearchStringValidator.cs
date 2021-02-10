using FluentValidation;
using System.Linq;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchStringValidator : AbstractValidator<string>
    {
        public SearchStringValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchString", () =>
            {
                RuleFor(x => x)
                    .NotEmpty()
                    .MaximumLength(50);
            });
        }
    }

    public class VoteValueValidator : AbstractValidator<int>
    {
        public VoteValueValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("VoteValue", () =>
            {
                RuleFor(x => x)
                    .GreaterThan(0)
                    .LessThan(6)
                    .Must(v => new[] { 1, 2, 3, 4, 5 }.Contains(v))
                    .WithMessage("Not valid vote. Vote value range: [1, 2, 3, 4, 5]");
            });
        }
    }
}