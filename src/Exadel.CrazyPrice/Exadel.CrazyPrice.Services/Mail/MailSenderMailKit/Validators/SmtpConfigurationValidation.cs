using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Configuration;
using Microsoft.Extensions.Options;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Validators
{
    public class SmtpConfigurationValidation : IValidateOptions<SmtpConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SmtpConfiguration options)
        {
            if (options == null)
            {
                return ValidateOptionsResult.Fail("The Smtp configuration must be defined.");
            }

            if (options.Host.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The Host must be defined.");
            }

            if (options.Port < 0)
            {
                return ValidateOptionsResult.Fail("The Port must be defined and great than 0.");
            }

            //if (options.UseSsl)
            //{
            //    return ValidateOptionsResult.Fail("The UseSsl must be defined.");
            //}

            if (options.UserName.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The UserName must be defined.");
            }

            if (options.Password.IsNullOrEmpty())
            {
                return ValidateOptionsResult.Fail("The Password must be defined.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
