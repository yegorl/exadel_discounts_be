using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchCriteriaValidator : AbstractValidator<SearchCriteria>
    {
        public SearchCriteriaValidator(IValidator<SearchAdvancedCriteria> searchAdvancedCriteriaValidator)
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchCriteria", () =>
            {
                RuleFor(x => x.SearchText)
                    .ValidCharacters(CharOptions.Letter | CharOptions.Number, " !&|");

                RuleFor(x => x.SearchAddressCountry)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.SearchAddressCity)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(20)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.SearchPersonId)
                    .NotEmpty();

                RuleFor(x => x.SearchDiscountOption)
                    .IsInEnum();

                RuleFor(x => x.SearchSortFieldOption)
                    .IsInEnum();

                RuleFor(x => x.SearchSortOption)
                    .IsInEnum();

                RuleFor(x => x.SearchPaginationPageNumber)
                    .Must(i => i > 0)
                    .WithMessage("The page number musts be great than 0.");

                RuleFor(x => x.SearchPaginationCountElementPerPage)
                    .Must(i => i > 4)
                    .WithMessage("The count element per page musts be great than 4.");

                RuleFor(x => x.SearchLanguage)
                    .IsInEnum();

                RuleFor(x => x.SearchAdvanced)
                    .SetValidator(searchAdvancedCriteriaValidator);
            });
        }
    }
}
