using Exadel.CrazyPrice.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Exadel.CrazyPrice.Common
{
    public static class CrazyPriceHost
    {
        public static void CheckLoggerAndStartHost(IHostBuilder hostBuilder,
            string initMessage, string errorInitMessage)
        {
            const string useLogging = "Logging:UseLogging";
            const string loggerName = "Logging:LoggerName";
            const string nLog = "nlog";
            const string serilog = "serilog";
            const string appsettings = "appsettings.json";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appsettings)
                .Build();

            if (!bool.TryParse(configuration.GetSection(useLogging).Value, out var hasLogging))
            {
                throw new ArgumentException(useLogging);
            }

            if (!hasLogging)
            {
                hostBuilder.Build().Run();
                return;
            }

            var usedLogger = configuration.GetSection(loggerName).Value.ToLowerInvariant();

            ICrazyPriceLogger logger = usedLogger switch
            {
                nLog => new NLogLogger(hostBuilder, configuration),
                serilog => new SerilogLogger(hostBuilder, configuration),
                _ => null
            };

            if (logger != null)
            {
                logger.UseLogger();
                logger.HostRun(initMessage, errorInitMessage);
            }
            else
            {
                throw new ArgumentException(loggerName);
            }
        }
    }
}
