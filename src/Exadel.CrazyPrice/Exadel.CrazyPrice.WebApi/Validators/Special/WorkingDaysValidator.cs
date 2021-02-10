using Exadel.CrazyPrice.Common.Extentions;
using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class WorkingDaysValidator : PropertyValidator, IWorkingDaysValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue;

            if (value == null)
            {
                return true;
            }

            if (value is not string valueAsString)
            {
                return false;
            }

            valueAsString = valueAsString.Trim();
            return valueAsString.Length != 0 && valueAsString.IsValidWorkingDays();
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The WorkingDaysOfTheWeek musts be format 0101010. First is monday etc. 7 digits. 1 is open, 0 is closed.";
        }
    }

    public interface IWorkingDaysValidator { }
}
