﻿using FluentValidation;

namespace Exadel.CrazyPrice.WebApi.Validators
{
    public class SearchStringValidator : AbstractValidator<string>
    {
        public SearchStringValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleSet("SearchString", () =>
            {
                RuleFor(x => x)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);
            });
        }
    }
}