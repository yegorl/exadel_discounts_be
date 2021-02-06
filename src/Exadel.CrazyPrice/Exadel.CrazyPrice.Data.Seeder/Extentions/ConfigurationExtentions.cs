using System.Linq;
using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;

namespace Exadel.CrazyPrice.Data.Seeder.Extentions
{
    /// <summary>
    /// Represents extentions for gets values from сonfiguration.
    /// </summary>
    public static class ConfigurationExtentions
    {
        /// <summary>
        /// Gets string value from сonfiguration. When exceptionOrDefault is true and is cast error throws exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="exceptionOrDefault"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringWithValue(this IConfiguration configuration, string key, bool exceptionOrDefault = true, string defaultValue = "")
        {
            return configuration.GetSection(key).Value.ToStringWithValue(exceptionOrDefault, defaultValue);
        }

        /// <summary>
        /// Gets bool value from сonfiguration. When exceptionOrDefault is true and is cast error throws exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="exceptionOrDefault"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this IConfiguration configuration, string key, bool exceptionOrDefault = true, bool defaultValue = false)
        {
            return configuration.GetSection(key).Value.ToBool(exceptionOrDefault, defaultValue);
        }

        /// <summary>
        /// Gets uint value from сonfiguration. When exceptionOrDefault is true and is cast error throws exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="exceptionOrDefault"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUint(this IConfiguration configuration, string key, bool exceptionOrDefault = true, uint defaultValue = 0)
        {
            return configuration.GetSection(key).Value.ToUint(exceptionOrDefault, defaultValue);
        }

        /// <summary>
        /// Gets string newValue from value when keys exist in possibleValues.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string OverLoadString(this string value, string newValue, string[] possibleValues, params string[] keys)
        {
            return KeysExist(possibleValues, keys) ? newValue : value;
        }

        /// <summary>
        /// Gets bool newValue from value when keys exist in possibleValues. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool OverLoadBool(this bool value, bool newValue, string[] possibleValues, params string[] keys)
        {
            return KeysExist(possibleValues, keys) ? newValue : value;
        }

        /// <summary>
        /// Gets uint newValue from value when keys exist in possibleValues. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static uint OverLoadUint(this uint value, uint newValue, string[] possibleValues, params string[] keys)
        {
            return KeysExist(possibleValues, keys) ? newValue : value;
        }

        private static bool KeysExist(string[] possibleValues, params string[] keys)
        {
            
            return keys.Any(k => string.Join("(|)", possibleValues).ToLowerInvariant().Split("(|)").Contains(k.ToLowerInvariant()));
        }
    }
}
