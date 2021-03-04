using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Services.Mail.MailBuilder;
using Exadel.CrazyPrice.Services.Mail.MailBuilder.Models.Option;
using MimeKit;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Extentions
{
    public static class MailBuilderExtentions
    {
        public static MimeEntity GetMessageBody(this IMailBuilder body,
            MailBodyOption mailBodyOption, LanguageOption languageOption, object content) =>
            new BodyBuilder()
            {
                HtmlBody = body.GetBody(mailBodyOption, languageOption, content)
            }.ToMessageBody();
    }
}
