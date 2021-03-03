using System.Threading.Tasks;

namespace Exadel.CrazyPrice.Services.Mail.MailClient
{
    /// <summary>
    /// Represents Mail Client.
    /// </summary>
    public interface IMailClient
    {
        /// <summary>
        /// Sends message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendAsync(object message);
    }
}
