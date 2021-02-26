using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;
using System;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UpsertDiscountValidator : AbstractValidator<UpsertDiscountRequest>
    {
        public UpsertDiscountValidator(IValidator<Company> companyValidator,
            IValidator<Address> addressValidator, IValidator<PromocodeOptions> promocodeOptionsValidator)
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("UpsertDiscount", () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(200)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ,.-");

                RuleFor(x => x.Description)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(5000)
                    .ValidCharacters(CharOptions.Letter |
                                     CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " ,.-");

                RuleFor(x => x.AmountOfDiscount == null ? null : (decimal?)(Math.Truncate((decimal)x.AmountOfDiscount * 100) / 100))
                    .Must(x => x == null || x >= 0)
                    .WithMessage("The AmountOfDiscount musts be null or great than 0 or equals 0 and less than 100 or equals 100.");

                RuleFor(x => x.StartDate)
                    .Must(x => x == null || x > "01.01.2010 00:00:00".GetUtcDateTime())
                    .WithMessage("The StartDate musts be null or great than 01.01.2010 00:00:00");

                RuleFor(x => new { x.StartDate, x.EndDate })
                    .Must(x => x.EndDate == null || (x.StartDate != null && x.EndDate > x.StartDate))
                    .WithMessage("The EndDate musts be null or great than StartDate");

                RuleFor(x => string.Join(" ", x.Tags))
                    .NotEmpty()
                    .ValidCharacters(CharOptions.Letter | CharOptions.Digit, " ");

                RuleFor(x => x.Company)
                    .SetValidator(companyValidator);

                RuleFor(x => x.Address)
                    .SetValidator(addressValidator);

                RuleFor(x => x.WorkingDaysOfTheWeek)
                    .NotEmpty()
                    .ValidWorkingDays();

                RuleFor(x => x.PromocodeOptions)
                    .SetValidator(promocodeOptionsValidator);

                When(x => !x.Translations.IsEmpty(), () =>
                {
                    RuleForEach(x => x.Translations)
                    .ChildRules(i =>
                    {
                        i.CascadeMode = CascadeMode.Stop;

                        i.When(t => !t.Name.IsNullOrEmpty(), () =>
                        {
                            i.RuleFor(t => t.Name)
                                .MinimumLength(3)
                                .MaximumLength(200)
                                .ValidCharacters(CharOptions.Letter |
                                                 CharOptions.Number |
                                                 CharOptions.Symbol, " ,.-");
                        });

                        i.When(t => !t.Description.IsNullOrEmpty(), () =>
                        {
                            i.RuleFor(t => t.Description)
                            .NotEmpty()
                            .MinimumLength(3)
                            .MaximumLength(5000)
                            .ValidCharacters(CharOptions.Letter |
                                             CharOptions.Number |
                                             CharOptions.Punctuation |
                                             CharOptions.Symbol, " ,.-");
                        });

                        i.When(t => t.Address != null, () =>
                         {
                             i.RuleFor(t => t.Address.Country)
                                 .NotEmpty()
                                 .MinimumLength(3)
                                 .MaximumLength(50)
                                 .ValidCharacters(CharOptions.Letter, " -");

                             i.RuleFor(t => t.Address.City)
                                 .NotEmpty()
                                 .MinimumLength(3)
                                 .MaximumLength(30)
                                 .ValidCharacters(CharOptions.Letter, " -");

                             i.RuleFor(t => t.Address.Street)
                                 .NotEmpty()
                                 .MinimumLength(3)
                                 .MaximumLength(300)
                                 .ValidCharacters(CharOptions.Letter |
                                                  CharOptions.Number |
                                                  CharOptions.Punctuation |
                                                  CharOptions.Symbol, " ,.-");
                         });

                        i.When(t => t.Company != null, () =>
                          {
                              i.RuleFor(t => t.Company.Name)
                                  .NotEmpty()
                                  .MinimumLength(3)
                                  .MaximumLength(200)
                                  .ValidCharacters(CharOptions.Letter |
                                                   CharOptions.Number |
                                                   CharOptions.Punctuation |
                                                   CharOptions.Symbol, " ,.-");

                              i.RuleFor(t => t.Company.Description)
                                  .NotEmpty()
                                  .MinimumLength(3)
                                  .MaximumLength(2000)
                                  .ValidCharacters(CharOptions.Letter |
                                                   CharOptions.Number |
                                                   CharOptions.Punctuation |
                                                   CharOptions.Symbol, " ,.-");
                          });

                        i.When(t => !t.Tags.IsNullOrEmpty(), () =>
                             {
                                 i.RuleFor(t => string.Join(" ", t.Tags))
                                 .NotEmpty()
                                 .ValidCharacters(CharOptions.Letter | CharOptions.Digit, " ");
                             });
                    });
                });
            });
        }
    }
}
