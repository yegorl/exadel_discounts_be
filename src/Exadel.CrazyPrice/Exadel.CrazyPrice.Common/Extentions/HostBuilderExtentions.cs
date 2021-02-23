using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Serilog;
using System;

namespace Exadel.CrazyPrice.Common.Extentions
{
    public static class HostBuilderExtentions
    {
        public static IHostBuilder SetupLogger(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration(hostingContext.Configuration.ParseSection("Logging"));

                if (hostingContext.HasLog("LogToSerilog"))
                {
                    Environment.SetEnvironmentVariable("BASEDIR", AppDomain.CurrentDomain.BaseDirectory);
                    logging.AddSerilog(new LoggerConfiguration()
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .CreateLogger());
                }

                if (hostingContext.HasLog("LogToNLog"))
                {
                    logging.AddNLogWeb();
                }
            });
        }

        private static bool HasLog(this HostBuilderContext hostingContext, string kindLod)
        {
            if (!bool.TryParse(hostingContext.Configuration.GetString(kindLod), out var hasLog))
            {
                throw new ArgumentException(kindLod);
            }

            return hasLog;
        }
    }
}
