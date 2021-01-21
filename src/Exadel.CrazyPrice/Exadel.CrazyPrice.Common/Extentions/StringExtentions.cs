using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Represents extentions for correcting string.
    /// </summary>
    public static class StringExtentions
    {

        /// <summary>
        /// Removes 2 and more space. Trim.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReplaceTwoAndMoreSpaceByOne(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            s = s.Trim();
            if (!s.Contains("  "))
            {
                return s;
            }

            s = ' ' + s;
            var stbBuilder = new StringBuilder();
            for (var i = 1; i <= s.Length - 1; i++)
            {
                if (s[i] == ' ' && s[i - 1] == ' ')
                {
                    continue;
                }
                stbBuilder.Append(s[i]);
            }
            return stbBuilder.ToString();
        }

        /// <summary>
        /// Gets words separated by a space.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetOnlyLettersDigitsAndOneSpace(this string s) =>
            s
                .Where(c => char.IsLetter(c) || char.IsDigit(c) || c == ' ')
                .Aggregate(string.Empty, (current, c) => current + c).ReplaceTwoAndMoreSpaceByOne();

        /// <summary>
        /// Gets DateTime from a string with format "dd.MM.yyyy HH:mm:ss"
        /// </summary>
        /// <param name="stringDate"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeInvariant(this string stringDate)
        {
            if (string.IsNullOrEmpty(stringDate))
            {
                throw new ArgumentNullException(nameof(stringDate));
            }

            return DateTime.ParseExact(stringDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
