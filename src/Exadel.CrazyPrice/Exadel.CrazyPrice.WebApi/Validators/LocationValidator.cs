using Exadel.CrazyPrice.Common.Models;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("Location", () =>
            {
                RuleFor(x => new { x.Latitude, x.Longitude })
                    .Must(x => x.Longitude == default && x.Latitude == default ||
                               (x.Latitude >= -90 && x.Latitude <= 90) && (x.Longitude >= -180 && x.Longitude <= 180))
                    .WithMessage("The Latitude musts be -90..90 and the Longitude musts be -180..180, or the Latitude and the Longitude musts be null.");
            });
        }
    }
}