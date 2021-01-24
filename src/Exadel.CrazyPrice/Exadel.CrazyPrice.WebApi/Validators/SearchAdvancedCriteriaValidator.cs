using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchAdvancedCriteriaValidator : AbstractValidator<SearchAdvancedCriteria>
    {
        public SearchAdvancedCriteriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchAdvanced", () =>
            {
                RuleFor(x => x.SearchStartDate)
                    .InjectValidator((services, context) => (IValidator<SearchDateCriteria>)services.GetService(typeof(IValidator<SearchDateCriteria>)));

                RuleFor(x => x.SearchEndDate)
                    .InjectValidator((services, context) => (IValidator<SearchDateCriteria>)services.GetService(typeof(IValidator<SearchDateCriteria>)));

                RuleFor(x => x.SearchAmountOfDiscount)
                    .InjectValidator((services, context) => (IValidator<SearchAmountOfDiscountCriteria>)services.GetService(typeof(IValidator<SearchAmountOfDiscountCriteria>)));

                RuleFor(x => x.SearchRatingTotal)
                    .InjectValidator((services, context) => (IValidator<SearchRatingTotalCriteria>)services.GetService(typeof(IValidator<SearchRatingTotalCriteria>)));
            });
        }
    }
}
