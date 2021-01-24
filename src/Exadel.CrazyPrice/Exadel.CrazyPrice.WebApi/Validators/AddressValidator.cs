using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
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
                    .Transform(d => d.GetValidContent(CharOptions.Letter, " -"))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);

                RuleFor(x => x.City)
                    .Transform(d => d.GetValidContent(CharOptions.Letter, " -"))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(20);

                RuleFor(x => x.Street)
                    .Transform(d => d.GetValidContent(CharOptions.Letter | CharOptions.Number | CharOptions.Punctuation | CharOptions.Symbol, " "))
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(40);

                RuleFor(x => x.Location)
                    .InjectValidator((services, context) => (IValidator<Location>)services.GetService(typeof(IValidator<Location>)));
            });
        }
    }
}