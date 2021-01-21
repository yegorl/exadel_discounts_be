using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchCriteriaValidator : AbstractValidator<SearchCriteria>
    {
        public SearchCriteriaValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchCriteria", () =>
            {
                RuleFor(x => x.SearchText)
                    .Transform(n => n.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(128);

                RuleFor(x => x.SearchCountry)
                    .Transform(d => d.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(15);

                RuleFor(x => x.SearchCity)
                    .Transform(d => d.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(15);

                RuleFor(x => x.SearchDiscountOption)
                    .IsInEnum();

                RuleFor(x => x.SearchPersonId)
                    .NotEmpty();

                RuleFor(x => x.SearchSortFieldOption)
                    .IsInEnum();

                RuleFor(x => x.SearchSortOption)
                    .IsInEnum();

                RuleFor(x => x.SearchPageNumber)
                    .Must(i => i > 0);

                RuleFor(x => x.SearchCountElementPerPage)
                    .Must(i => i > 4);
            });
        }
    }
}
