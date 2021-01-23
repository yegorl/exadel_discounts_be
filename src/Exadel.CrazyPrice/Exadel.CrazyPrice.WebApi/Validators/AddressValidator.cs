using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Address", () =>
            {
                RuleFor(x => x.Country)
                    .Transform(d => d.GetOnlyLettersDigitsAndOneSpace())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);

                RuleFor(x => x.City)
                    .Transform(d => d.ReplaceTwoAndMoreSpaceByOne())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(20);

                RuleFor(x => x.Street)
                    .Transform(d => d.ReplaceTwoAndMoreSpaceByOne())
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(40);

                RuleFor(x => x.Location)
                    .InjectValidator((services, context) => (IValidator<Location>)services.GetService(typeof(IValidator<Location>)));
            });
        }
    }
}