using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class FirstLetterValidator : PropertyValidator, IFirstLetterValidator
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
            if (valueAsString.Length == 0)
            {
                return false;
            }

            var first = valueAsString.Substring(0, 1);

            return first == first.GetValidContent(CharOptions.Letter);
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The first character must be a letter.";
        }
    }

    public interface IFirstLetterValidator { }
}
