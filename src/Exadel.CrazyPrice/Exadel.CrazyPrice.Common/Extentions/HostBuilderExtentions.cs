using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Serilog;
using System;

namespace Exadel.CrazyPrice.Common.Extentions
{
    public static class HostBuilderExtentions
    {
        public static IHostBuilder UseLogger(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                if (HasLog(hostingContext, "LogToSerilog"))
                {
                    Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);
                    logging.AddSerilog(new LoggerConfiguration()
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .CreateLogger());
                }

                if (HasLog(hostingContext, "LogToNLog"))
                {
                    logging.AddNLog();
                }
            });
        }

        private static bool HasLog(HostBuilderContext hostingContext, string kindLod)
        {
            if (!bool.TryParse(hostingContext.Configuration.GetSection(kindLod).Value, out var hasLog))
            {
                throw new ArgumentException(kindLod);
            }
            return hasLog;
        }
    }
}
