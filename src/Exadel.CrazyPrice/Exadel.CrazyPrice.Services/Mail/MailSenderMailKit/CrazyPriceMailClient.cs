using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Services.Mail.MailClient;
using Exadel.CrazyPrice.Services.Mail.MailSenderMailKit.Configuration;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Polly;
using Polly.Retry;

namespace Exadel.CrazyPrice.Services.Mail.MailSenderMailKit
{
    public class CrazyPriceMailClient : IMailClient, IDisposable
    {
        private readonly ILogger<CrazyPriceMailClient> _logger;
        private readonly SmtpConfiguration _configuration;
        private readonly SmtpClient _client;
        private bool _disposed;

        private readonly object _syncRoot = new();

        public CrazyPriceMailClient(IOptionsMonitor<SmtpConfiguration> options, ILogger<CrazyPriceMailClient> logger)
        {
            _logger = logger;
            _configuration = options.CurrentValue;
            _client = new SmtpClient();
        }

        public async Task SendAsync(object message)
        {
            var connected = await ConnectAsync();

            if (!connected)
            {
                while (!TryConnect())
                {
                    await Task.Delay(TimeSpan.FromSeconds(_configuration.DelaySecondsBeforeRepeat));
                }
            }

            await _client.SendAsync((MimeMessage)message);
        }

        private async Task<bool> ConnectAsync()
        {
            if (_disposed)
            {
                return false;
            }

            await _client.ConnectAsync(_configuration.Host, _configuration.Port, _configuration.UseSsl);
            await _client.AuthenticateAsync(_configuration.UserName, _configuration.Password);

            return IsConnected;
        }

        private bool IsConnected => _client.IsAuthenticated;

        private bool TryConnect()
        {
            _logger.LogInformation("CrazyPriceMailClient is trying to connect to SMTP Server {Host}...", _configuration.Host);

            lock (_syncRoot)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .WaitAndRetry(_configuration.RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                        {
                            _logger.LogWarning(ex, "CrazyPriceMailClient could not connect to SMTP server '{Host}' after {TimeOut}s ({ExceptionMessage})", _configuration.Host, $"{time.TotalSeconds:n1}", ex.Message);
                        }
                    );

                policy.Execute(async () =>
                {
                    await ConnectAsync();
                });

                if (IsConnected)
                {
                    _logger.LogInformation("CrazyPriceMailClient acquired a persistent connection to SMTP server '{Host}'.", _configuration.Host);

                    return true;
                }

                _logger.LogCritical("CrazyPriceMailClient could not connect to SMTP server '{Host}'.", _configuration.Host);

                return false;
            }
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _client.DisconnectAsync(true);
            }

            _client?.Dispose();
            _disposed = true;
        }

        ~CrazyPriceMailClient()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
