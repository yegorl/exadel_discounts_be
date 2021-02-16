using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Exadel.CrazyPrice.Common.Extentions
{
    public static class ConfigurationExtentions
    {
        public static string GetString(this IConfiguration config, string key) =>
            config.GetSection(key).Value;

        public static string[] GetArrayString(this IConfiguration config, string key) =>
            config.GetSection(key).Get<string[]>().Distinct().ToArray();

        public static Uri GetUri(this IConfiguration config, string key) =>
            string.IsNullOrEmpty(config.GetString(key)) ? null : new Uri(config.GetString(key), UriKind.Absolute);

        public static Dictionary<string, string> GetDictionaryString(this IConfiguration config, string key) =>
            config.GetSection(key).Get<Dictionary<string, string>>();
    }
}
