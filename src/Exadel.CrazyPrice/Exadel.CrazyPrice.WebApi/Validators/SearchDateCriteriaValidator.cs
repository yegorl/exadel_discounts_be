using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchDateCriteriaValidator : AbstractValidator<SearchDateCriteria>
    {
        public SearchDateCriteriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchDate", () =>
            {
                RuleFor(x => x.SearchDateFirst)
                    .Must(x => x == null || x > "01.01.2010 00:00:00".GetDateTimeInvariant())
                    .WithMessage("The SearchDateFirst musts be null or great than 01.01.2010 00:00:00.");

                RuleFor(x => new { x.SearchDateFirst, x.SearchDateLast })
                    .Must(x => x.SearchDateLast == null || (x.SearchDateFirst != null && x.SearchDateLast > x.SearchDateFirst))
                    .WithMessage("The SearchDateLast musts be null or great than SearchDateFirst.");
            });
        }
    }
}
