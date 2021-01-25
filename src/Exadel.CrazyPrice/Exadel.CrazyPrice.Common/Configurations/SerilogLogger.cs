using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Exadel.CrazyPrice.Common.Configurations
{
    public class SerilogLogger : ICrazyPriceLogger
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IConfiguration _configuration;

        public SerilogLogger(IHostBuilder hostBuilder, IConfiguration configuration)
        {
            _hostBuilder = hostBuilder;
            _configuration = configuration;
        }

        public IHostBuilder UseLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            return _hostBuilder
                        .ConfigureLogging(logging =>
                        {
                            logging.ClearProviders();
                            logging.SetMinimumLevel(LogLevel.Trace);
                        })
                        .UseSerilog();
        }

        public void HostRun(string initMessage, string errorInitMessage)
        {
            try
            {
                Log.Information(initMessage);
                _hostBuilder.Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, errorInitMessage);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
