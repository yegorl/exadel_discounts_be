using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchRatingTotalCriteriaValidator : AbstractValidator<SearchRatingTotalCriteria>
    {
        public SearchRatingTotalCriteriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchRatingTotal", () =>
            {
                RuleFor(x => x.SearchRatingTotalMin)
                    .Must(x => x == null || x >= 0 && x <= 5)
                    .WithMessage("The SearchRatingTotalMin musts be null or [0..5].");

                RuleFor(x => new { x.SearchRatingTotalMin, x.SearchRatingTotalMax })
                    .Must(x => x.SearchRatingTotalMax == null || (x.SearchRatingTotalMin != null && x.SearchRatingTotalMax > x.SearchRatingTotalMin))
                    .WithMessage("The SearchRatingTotalMax musts be null or great than SearchAmountOfDiscountMin and [0..5].");
            });
        }
    }
}
