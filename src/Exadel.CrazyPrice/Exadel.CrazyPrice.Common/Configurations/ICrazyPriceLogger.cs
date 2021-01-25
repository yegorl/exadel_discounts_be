using Microsoft.Extensions.Hosting;

namespace Exadel.CrazyPrice.Common.Configurations
{
    public interface ICrazyPriceLogger
    {
        IHostBuilder UseLogger();

        void HostRun(string initMessage, string errorInitMessage);
    }
}