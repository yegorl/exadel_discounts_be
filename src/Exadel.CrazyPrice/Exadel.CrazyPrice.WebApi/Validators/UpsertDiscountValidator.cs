using Exadel.CrazyPrice.Common.Models.Request;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UpsertDiscountValidator : AbstractValidator<UpsertDiscountRequest>
    {
        public UpsertDiscountValidator()
        {
            CascadeMode = CascadeMode.Stop;

            // !! Add Rules
        }
    }
}
