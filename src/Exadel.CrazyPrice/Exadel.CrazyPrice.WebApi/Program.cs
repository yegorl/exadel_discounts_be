using Exadel.CrazyPrice.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Exadel.CrazyPrice.WebApi
{
    /// <summary>
    /// Determines the entry point into the program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts web application.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var usedLogger = configuration.GetSection("UsedLogger").Value.ToLowerInvariant();

            ICrazyPriceLogger logger = usedLogger switch
            {
                "nlog" => new NLogLogger(CreateHostBuilder(args), configuration),
                "serilog" => new SerilogLogger(CreateHostBuilder(args), configuration),
                _ => null
            };

            if (logger != null)
            {
                logger.UseLogger();
                logger.HostRun("WebApi started.", "WebApi stopped of because error.");
            }
            else
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        /// <summary>
        /// Creates HostBuilder using the default Builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
