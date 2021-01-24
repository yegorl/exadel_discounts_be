using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation;
using static Exadel.CrazyPrice.Common.Extentions.EnumExtentions;

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
                    .Transform(n => string.IsNullOrEmpty(n) ? n : n.GetValidContent(CharOptions.Letter | CharOptions.Number, " !&|"));

                RuleFor(x => x.SearchAddressCountry)
                    .Transform(c => string.IsNullOrEmpty(c) ? c : c.GetValidContent(CharOptions.Letter, " -"));

                RuleFor(x => x.SearchAddressCity)
                    .Transform(d => d.GetValidContent(CharOptions.Letter, " -"))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(15);

                RuleFor(x => x.SearchPersonId)
                    .NotEmpty();

                RuleFor(x => x.SearchDiscountOption)
                    .IsInEnum()
                    .WithMessage($"The valid value musts be {GetStringFrom<DiscountOption>("$k: $v")}.");

                RuleFor(x => x.SearchSortFieldOption)
                    .IsInEnum()
                    .WithMessage($"The valid value musts be {GetStringFrom<SortFieldOption>("$k: $v")}.");

                RuleFor(x => x.SearchSortOption)
                    .IsInEnum()
                    .WithMessage($"The valid value musts be {GetStringFrom<SortOption>("$k: $v")}.");

                RuleFor(x => x.SearchPaginationPageNumber)
                    .Must(i => i > 0);

                RuleFor(x => x.SearchPaginationCountElementPerPage)
                    .Must(i => i > 4);

                RuleFor(x => x.SearchLanguage)
                    .IsInEnum()
                    .WithMessage($"The valid value musts be {GetStringFrom<LanguageOption>("$k: $v")}.");

                RuleFor(x => x.SearchAdvanced)
                    .InjectValidator((services, context) => (IValidator<SearchAdvancedCriteria>)services.GetService(typeof(IValidator<SearchAdvancedCriteria>)));
            });
        }
    }
}
