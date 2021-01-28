using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class NotContainsSpaceValidator : PropertyValidator, INotContainsSpaceValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue;

            if (value == null)
            {
                return true;
            }

            return value is string valueAsString && !valueAsString.Contains(' ');
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The value cannot contain spaces.";
        }
    }

    public interface INotContainsSpaceValidator { }
}
