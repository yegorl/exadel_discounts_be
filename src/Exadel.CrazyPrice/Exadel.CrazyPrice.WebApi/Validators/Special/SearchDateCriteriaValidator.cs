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

            if (value.SearchStartDate == null || value.SearchEndDate == null)
            {
                return true;
            }

            return value.SearchEndDate > value.SearchStartDate;
        }

        protected override string GetDefaultMessageTemplate()
        {
            return "The SearchEndDate musts be null or great than SearchStartDate.";
        }
    }

    public interface ISearchDateTimeCriteriaValidator { }
}
