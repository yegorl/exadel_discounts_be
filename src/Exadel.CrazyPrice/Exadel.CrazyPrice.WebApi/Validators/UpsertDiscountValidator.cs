using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Request;
using FluentValidation;
using System;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UpsertDiscountValidator : AbstractValidator<UpsertDiscountRequest>
    {
        public UpsertDiscountValidator(IValidator<Company> companyValidator, IValidator<Address> addressValidator)
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("UpsertDiscount", () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Symbol, " ");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(1000)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ");

                RuleFor(x => x.AmountOfDiscount)
                    .Transform(d => d == null ? null : (decimal?)(Math.Truncate((decimal)d * 100) / 100))
                    .Must(x => x == null || x >= 0)
                    .WithMessage("The AmountOfDiscount musts be null or great than 0 or equals 0 and less than 100 or equals 100.");

                RuleFor(x => x.StartDate)
                    .Must(x => x == null || x > "01.01.2010 00:00:00".GetDateTimeInvariant())
                    .WithMessage("The StartDate musts be null or great than 01.01.2010 00:00:00");

                RuleFor(x => new { x.StartDate, x.EndDate })
                    .Must(x => x.EndDate == null || (x.StartDate != null && x.EndDate > x.StartDate))
                    .WithMessage("The EndDate musts be null or great than StartDate");

                RuleFor(x => x.Tags)
                    .Transform(t => string.Join(" ", t))
                    .NotEmpty()
                    .ValidCharacters(CharOptions.Letter | CharOptions.Number, " ");

                RuleFor(x => x.Company)
                    .SetValidator(companyValidator);

                RuleFor(x => x.Address)
                    .SetValidator(addressValidator);

                RuleFor(x => x.WorkingHours)
                    .NotEmpty()
                    .ValidWorkingDays();
            });
        }
    }
}
