using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchAmountOfDiscountCriteriaValidator : AbstractValidator<SearchAmountOfDiscountCriteria>
    {
        public SearchAmountOfDiscountCriteriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchAmountOfDiscount", () =>
            {
                RuleFor(x => x.SearchAmountOfDiscountMin)
                    .Must(x => x == null || x >= 0 && x <= 100)
                    .WithMessage("The SearchAmountOfDiscountMin musts be null or [0..100].");

                RuleFor(x => new { x.SearchAmountOfDiscountMin, x.SearchAmountOfDiscountMax })
                    .Must(x => x.SearchAmountOfDiscountMax == null || (x.SearchAmountOfDiscountMin != null && x.SearchAmountOfDiscountMax > x.SearchAmountOfDiscountMin))
                    .WithMessage("The SearchAmountOfDiscountMax musts be null or great than SearchAmountOfDiscountMin and [0..100].");
            });
        }
    }
}
