using Microsoft.Extensions.Hosting;

namespace Exadel.CrazyPrice.Common.Configurations
{
    public interface ILoggerConfiguration
    {
        public void UseLogger(IHostBuilder hostBuilder,
            string initMessage, string errorInitMessage, string logConfig);
    }
}
