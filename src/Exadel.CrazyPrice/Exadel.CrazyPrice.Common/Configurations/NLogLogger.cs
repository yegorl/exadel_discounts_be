using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Exadel.CrazyPrice.Common.Configurations
{
    public class NLogLogger : ILoggerConfiguration
    {
        public void UseLogger(IHostBuilder hostBuilder,
            string initMessage, string errorInitMessage, string nLogConfig)
        {
            var logger = NLogBuilder.ConfigureNLog(nLogConfig).GetCurrentClassLogger();
            try
            {
                logger.Debug(initMessage);
                hostBuilder
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .UseNLog()
                    .Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, errorInitMessage);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static void CreateLoggerAndRunHost(IHostBuilder hostBuilder,
            string initMessage, string errorInitMessage, string nLogConfig = "nlog.config")
        {
            new NLogLogger().UseLogger(hostBuilder, initMessage, errorInitMessage, nLogConfig);
        }
    }
}
