using Exadel.CrazyPrice.Common.Models;
using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class LocationValidator : PropertyValidator, ILocationValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue;

            if (value == null)
            {
                return true;
            }

            if (value is not Location valueAsLocation)
            {
                return false;
            }

            return valueAsLocation.Longitude == default && valueAsLocation.Latitude == default ||
                   (valueAsLocation.Latitude >= -90 && valueAsLocation.Latitude <= 90)
                   && (valueAsLocation.Longitude >= -180 && valueAsLocation.Longitude <= 180);
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The Latitude musts be -90..90 and the Longitude musts be -180..180, or the Latitude and the Longitude musts be null.";
        }
    }

    public interface ILocationValidator { }
}
