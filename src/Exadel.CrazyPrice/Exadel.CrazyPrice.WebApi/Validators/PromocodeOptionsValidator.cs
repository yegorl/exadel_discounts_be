using Exadel.CrazyPrice.Common.Models.Promocode;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class PromocodeOptionsValidator : AbstractValidator<PromocodeOptions>
    {
        public PromocodeOptionsValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("PromocodeOptions", () =>
            {
                RuleFor(x => x.CountSymbolsPromocode ?? 4)
                    .GreaterThan(3)
                    .LessThan(25);

                RuleFor(x => x.CountActivePromocodePerUser ?? 5)
                    .GreaterThan(0)
                    .LessThan(30);

                RuleFor(x => x.TimeLimitAddingInSeconds ?? 1)
                    .GreaterThan(0);

                RuleFor(x => x.DaysDurationPromocode ?? 1)
                    .GreaterThan(0);
            });
        }
    }
}