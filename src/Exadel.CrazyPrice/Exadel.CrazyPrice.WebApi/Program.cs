using System;
using Exadel.CrazyPrice.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Exadel.CrazyPrice.Common;

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
            CrazyPriceHost.CheckLoggerAndStartHost(CreateHostBuilder(args), 
                "WebApi started", 
                "WebApi stopped of because error.");
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
