using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class ValidCharactersValidator : PropertyValidator, IValidCharactersValidator
    {
        public ValidCharactersValidator(CharOptions charOptions, string specialChars)
        {
            CharOptions = charOptions;
            SpecialChars = specialChars;
        }

        public CharOptions CharOptions { get; }

        public string SpecialChars { get; }

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

            return valueAsString == valueAsString.GetValidContent(CharOptions, SpecialChars);
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The string contains invalid characters.";
        }
    }

    public interface IValidCharactersValidator
    {
        public CharOptions CharOptions { get; }

        public string SpecialChars { get; }
    }
}
