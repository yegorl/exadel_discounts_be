using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exadel.CrazyPrice.Common.Extentions
{
    public static class EnumExtentions
    {
        /// <summary>
        /// Gets the formatted string from enum.
        /// $k or $K - the key enum,
        /// $v or $V - the value enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static string GetStringFrom<T>(string formatString) where T : struct
        {
            if (string.IsNullOrEmpty(formatString))
            {
                throw new ArgumentNullException(formatString);
            }

            formatString = formatString.GetValidContent(CharOptions.Letter | CharOptions.Symbol, " :-=|").ToLowerInvariant();

            var valid = new[] { "$k",  "$v" };
            if (!valid.Any(s => formatString.Contains(s)))
            {
                throw new FormatException(formatString);
            }

            var kvp =
                (from object value in Enum.GetValues(typeof(T)) select new KeyValuePair<string, int>(((T)value).ToString(), (int)(object)(T)value)).ToList();

            var strBuilder = new StringBuilder();
            foreach (var (key, value) in kvp)
            {
                strBuilder.Append(formatString.Replace("$k", key).Replace("$v", value.ToString()) + ", ");
            }

            return strBuilder.ToString().TrimEnd(',', ' ');
        }
    }
}
