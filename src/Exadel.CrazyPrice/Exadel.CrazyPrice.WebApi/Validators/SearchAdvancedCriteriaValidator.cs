using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchAdvancedCriteriaValidator : AbstractValidator<SearchAdvancedCriteria>
    {
        public SearchAdvancedCriteriaValidator(IValidator<SearchAmountOfDiscountCriteria> searchAmountOfDiscountCriteriaValidator,
            IValidator<SearchRatingTotalCriteria> searchRatingTotalCriteriaValidator)
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchAdvanced", () =>
            {
                RuleFor(x => x.CompanyName)
                    .Must(x => x == null || x.Length > 2)
                    .WithMessage("The CompanyName musts be null or their length great than 2.");

                RuleFor(x => x.SearchDate)
                    .ValidSearchDate();

                RuleFor(x => x.SearchAmountOfDiscount)
                    .SetValidator(searchAmountOfDiscountCriteriaValidator);

                RuleFor(x => x.SearchRatingTotal)
                    .SetValidator(searchRatingTotalCriteriaValidator);
            });
        }
    }
}
