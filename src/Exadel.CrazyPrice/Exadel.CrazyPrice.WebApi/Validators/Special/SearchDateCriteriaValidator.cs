using Exadel.CrazyPrice.Common.Models.SearchCriteria;
using FluentValidation.Validators;

namespace Exadel.CrazyPrice.WebApi.Validators.Special
{
    public class SearchDateTimeCriteriaValidator : PropertyValidator, ISearchDateTimeCriteriaValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var objValue = context.PropertyValue;

            if (objValue == null)
            {
                return true;
            }

            if (objValue is not SearchDateCriteria value)
            {
                return false;
            }

            if (value.SearchDateFirst == null || value.SearchDateLast == null)
            {
                return true;
            }

            return value.SearchDateLast > value.SearchDateFirst;
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The SearchDateLast musts be null or great than SearchDateFirst.";
        }
    }

    public interface ISearchDateTimeCriteriaValidator { }
}
