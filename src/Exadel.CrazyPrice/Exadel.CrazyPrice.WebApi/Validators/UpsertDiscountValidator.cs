using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Request;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UpsertDiscountValidator : AbstractValidator<UpsertDiscountRequest>
    {
        public UpsertDiscountValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("UpsertDiscount", () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .Transform(d => d.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);

                RuleFor(x => x.Description)
                    .Transform(d => d.ReplaceTwoAndMoreSpaceByOne())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(1000);

                RuleFor(x => x.StartDate)
                    .Must(x => x == null || x > "01.01.2010 00:00:00".GetDateTimeInvariant())
                    .WithMessage("The StartDate musts be null or great than 01.01.2010 00:00:00");

                RuleFor(x => new { x.StartDate, x.EndDate })
                    .Must(x => x.EndDate == null || (x.StartDate != null && x.EndDate > x.StartDate))
                    .WithMessage("The EndDate musts be null or great than StartDate");

                RuleFor(x => x.Tags)
                    .Transform(t => string.Join(" ", t).GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty();

                RuleFor(x => x.Company)
                    .InjectValidator((services, context) => (IValidator<Company>)services.GetService(typeof(IValidator<Company>)));

                RuleFor(x => x.Address)
                    .InjectValidator((services, context) => (IValidator<Address>)services.GetService(typeof(IValidator<Address>)));

                RuleFor(x => x.WorkingHours)
                    .NotEmpty()
                    .Must(x => x.IsValidWorkingDays())
                    .WithMessage("The WorkingHours musts be format 0101010. First is monday etc. 7 digits. 1 is open, 0 is closed.");
            });
        }
    }
}
