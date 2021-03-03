﻿namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Configuration
{
    public class SmtpConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int RetryCount { get; set; }

        public int DelaySecondsBeforeRepeat { get; set; }
    }
}
