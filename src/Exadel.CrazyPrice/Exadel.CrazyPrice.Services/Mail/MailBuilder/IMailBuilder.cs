using Exadel.CrazyPrice.Common.Models.Option;
using Exadel.CrazyPrice.Services.Mail.MailBuilder.Models.Option;

namespace Exadel.CrazyPrice.Services.Mail.MailBuilder
{
    public interface IMailBuilder
    {
        string GetBody(MailBodyOption mailBodyOption, LanguageOption language, object content);
    }
}
