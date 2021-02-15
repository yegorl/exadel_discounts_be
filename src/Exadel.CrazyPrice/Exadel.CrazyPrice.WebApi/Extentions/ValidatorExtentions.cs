using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Validators.Special;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class ValidatorExtentions
    {
        public static IRuleBuilderOptions<T, TProperty> ValidCharacters<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, CharOptions charOptions, string specialChars)
        {
            return ruleBuilder.SetValidator(new ValidCharactersValidator(charOptions, specialChars));
        }

        public static IRuleBuilderOptions<T, TProperty> FirstLetter<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FirstLetterValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> FirstDigit<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new FirstDigitValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> Location<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new LocationValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> NotContainsSpace<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NotContainsSpaceValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> ValidSearchDate<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new SearchDateTimeCriteriaValidator());
        }

        public static IRuleBuilderOptions<T, TProperty> ValidWorkingDays<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new WorkingDaysValidator());

        }
    }
}
