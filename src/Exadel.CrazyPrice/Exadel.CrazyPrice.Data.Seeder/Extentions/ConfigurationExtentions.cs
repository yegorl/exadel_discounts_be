using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Exadel.CrazyPrice.Data.Seeder.Extentions
{
    /// <summary>
    /// Represents extentions for gets values from сonfiguration.
    /// </summary>
    public static class ConfigurationExtentions
    {
        /// <summary>
        /// Gets string value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringWithValue(this IConfiguration configuration, string key, string defaultValue = "", bool raiseException = true)
        {
            return configuration.GetSection(key).Value.ToStringWithValue(defaultValue, raiseException);
        }

        /// <summary>
        /// Gets bool value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this IConfiguration configuration, string key, bool defaultValue = false, bool raiseException = true)
        {
            return configuration.GetSection(key).Value.ToBool(defaultValue, raiseException);
        }

        /// <summary>
        /// Gets uint value from сonfiguration. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUint(this IConfiguration configuration, string key, uint defaultValue = 0, bool raiseException = true)
        {
            return configuration.GetSection(key).Value.ToUint(defaultValue, raiseException);
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
