using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Common.Models.Request;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("UpdateUser", () =>
            {
                RuleFor(x => x.Password)
                    .Empty()
                    .When(x=>x.Password.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator)
                    .MinimumLength(6)
                    .MaximumLength(32)
                    .NotContainsSpace()
                    .ValidCharacters(CharOptions.Digit | CharOptions.Letter, "")
                    .When(x => !x.Password.IsNullOrEmpty());

                RuleFor(x => x.PhoneNumber)
                    .Empty()
                    .When(x => x.PhoneNumber.IsNullOrEmpty(), ApplyConditionTo.CurrentValidator)
                    .MinimumLength(6)
                    .MaximumLength(40)
                    .ValidCharacters(CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " -#.")
                    .When(x => !x.PhoneNumber.IsNullOrEmpty());

                RuleFor(x => x.PhotoUrl)
                    .Must(s => (Uri.TryCreate(s, UriKind.Absolute, out var outUri) &&
                               (outUri.Scheme == Uri.UriSchemeHttp ||
                                outUri.Scheme == Uri.UriSchemeHttps)) || s.IsNullOrEmpty())
                    .WithErrorCode("Invalid PhotoUrl");
            });
        }
    }
}
