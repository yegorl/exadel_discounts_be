using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Exadel.CrazyPrice.Common.Extentions
{
    public static class ConfigurationExtentions
    {
        public static string[] GetOrigins(this IConfiguration config) =>
            config.GetSection("Auth:Origins").Get<string[]>().Distinct().ToArray();

        public static string GetIssuerUrl(this IConfiguration config) =>
            config.GetOption("Auth:IssuerUrl");

        public static string GetPolicyName(this IConfiguration config) =>
            config.GetOption("Auth:PolicyName");

        public static string GetOption(this IConfiguration config, string key) =>
            config.GetSection(key).Value;
    }
}
