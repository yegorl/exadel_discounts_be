using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Services.Common.IntegrationEvents.Models;
using Exadel.CrazyPrice.Services.Mail.MailBuilder.Models.Option;
using Exadel.CrazyPrice.Services.Mail.MailBuilder.Resources;
using System;
using System.Text;

namespace Exadel.CrazyPrice.Services.Mail.MailBuilder
{
    public class MailBuilder : IMailBuilder
    {
        public string GetBody(MailBodyOption mailBodyOption, LanguageOption language, object content) =>
            mailBodyOption switch
            {
                MailBodyOption.Company => GetMailBodyForCompany(language, content),
                MailBodyOption.User => GetMailBodyForUser(language, content),
                _ => throw new ArgumentOutOfRangeException(nameof(mailBodyOption), mailBodyOption, null)
            };

        private string GetMailBodyForCompany(LanguageOption language, object content)
        {
            var mailContent = (CompanyMailContent)content;

            var builder = language switch
            {
                LanguageOption.Ru => new StringBuilder(Templates.CompanyMailBodyTemplateRu),
                _ => new StringBuilder(Templates.CompanyMailBodyTemplateEn)
            };

            builder.Replace("{{CompanyMail}}", mailContent.Company.Mail)
                .Replace("{{Service}}", mailContent.DiscountName)
                .Replace("{{PromoCode}}", mailContent.PromocodeValue);

            return builder.ToString();
        }

        private string GetMailBodyForUser(LanguageOption language, object content)
        {
            var mailContent = (UserMailContent)content;

            var builder = language switch
            {
                LanguageOption.Ru => new StringBuilder(Templates.UserMailBodyTemplateRu),
                _ => new StringBuilder(Templates.UserMailBodyTemplateEn),
            };

            builder.Replace("{{UserName}}", mailContent.Employee.Name)
                .Replace("{{PromoCode}}", mailContent.PromocodeValue)
                .Replace("{{Service}}", mailContent.DiscountName);

            return builder.ToString();
        }
    }
}
