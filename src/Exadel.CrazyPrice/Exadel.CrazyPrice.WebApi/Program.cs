using Exadel.CrazyPrice.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
            NLogLogger.CreateLoggerAndRunHost(CreateHostBuilder(args),
                "Init WebApi", "Stopped WebApi because of exception.");
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
