using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.WebApi.Extentions;
using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("UpsertUser", () =>
            {
                RuleFor(x => x.Id)
                    .NotEmpty();

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(30)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.Surname)
                    .NotEmpty()
                    .MinimumLength(2)
                    .MaximumLength(40)
                    .ValidCharacters(CharOptions.Letter, " -");

                RuleFor(x => x.Mail)
                    .NotEmpty()
                    .MinimumLength(6)
                    .MaximumLength(50)
                    .EmailAddress()
                    .NotContainsSpace();


                RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .MinimumLength(6)
                    .MaximumLength(40)
                    .ValidCharacters(CharOptions.Number |
                                     CharOptions.Punctuation |
                                     CharOptions.Symbol, " -#.");
            });
        }
    }
}