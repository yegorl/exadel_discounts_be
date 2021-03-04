namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Helpers
{
    public static class ApplicationInfo
    {
        public static string ApplicationName
        {
            get
            {
                var startupNamespace = typeof(Startup).Namespace;
                return startupNamespace;
            }
        }
    }
}
