using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Represents extentions for gets values from сonfiguration.
    /// </summary>
    public static class ConfigurationExtentions
    {
        /// <summary>
        /// Gets string value.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this IConfiguration config, string key) =>
            config.ParseSection(key).Value;

        /// <summary>
        /// Gets string[].
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetArrayString(this IConfiguration config, string key) =>
            config.ParseSection(key).Get<string[]>().Distinct().ToArray();

        /// <summary>
        /// Gets Uri.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Uri GetUri(this IConfiguration config, string key) =>
            string.IsNullOrEmpty(config.GetString(key)) ? null : new Uri(config.GetString(key), UriKind.Absolute);

        /// <summary>
        /// Gets Dictionary string.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryString(this IConfiguration config, string key) =>
            config.ParseSection(key).Get<Dictionary<string, string>>();

        /// <summary>
        /// Gets string value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringWithValue(this IConfiguration configuration, string key, string defaultValue = "", bool raiseException = true) =>
            configuration.GetString(key).ToStringWithValue(defaultValue, raiseException);

        /// <summary>
        /// Gets bool value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this IConfiguration configuration, string key, bool defaultValue = false, bool raiseException = true) =>
            configuration.GetString(key).ToBool(defaultValue, raiseException);

        /// <summary>
        /// Gets uint value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUint(this IConfiguration configuration, string key, uint defaultValue = 0, bool raiseException = true) =>
            configuration.GetString(key).ToUint(defaultValue, raiseException);

        /// <summary>
        /// Gets IConfigurationSection from сonfiguration. When raiseException is true and is cast error raises exception.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection ParseSection(this IConfiguration configuration, string key)
        {
            var section = configuration.GetSection(key);
            if (section.Exists())
            {
                return section;
            }
            else
            {
                throw new ArgumentException($"'{key}' is not exists.");
            }
        }
    }
}
