using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;
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
        /// Gets DateTime from a string with format "dd.MM.yyyy HH:mm:ss".
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

        /// <summary>
        /// Gets true if string is valid WorkingDays otherwise false.
        /// </summary>
        /// <param name="stringWorkingDays"></param>
        /// <returns></returns>
        public static bool IsValidWorkingDays(this string stringWorkingDays)
        {
            if (string.IsNullOrEmpty(stringWorkingDays) || stringWorkingDays.Length != 7)
            {
                return false;
            }

            var chars = new[] { '0', '1' };
            return stringWorkingDays.All(stringWorkingDay => chars.Contains(stringWorkingDay));
        }

        /// <summary>
        /// Gets string what two and more chars replaced by some one.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string ReplaceTwoAndMoreCharsBySomeOne(this string s, string chars)
        {
            if (string.IsNullOrEmpty(chars))
            {
                throw new ArgumentNullException(nameof(chars));
            }

            return s.ReplaceTwoAndMoreCharsBySomeOne(chars.ToCharArray());
        }

        /// <summary>
        /// Gets string what two and more chars replaced by some one.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string ReplaceTwoAndMoreCharsBySomeOne(this string s, char[] chars)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (chars == null || chars.Length == 0)
            {
                throw new ArgumentNullException(nameof(chars));
            }

            s = s.Trim(chars);

            if (s.Length == 0)
            {
                return string.Empty;
            }

            var charsBase = s.ToCharArray();

            foreach (var c in chars)
            {
                for (var i = 1; i <= charsBase.Length - 2; i++)
                {
                    if (charsBase[i] == char.MinValue || charsBase[i - 1] != c || charsBase[i] != c)
                    {
                        continue;
                    }

                    charsBase[i - 1] = char.MinValue;
                }
            }

            var strBuilder = new StringBuilder(charsBase.Length);
            foreach (var c in charsBase)
            {
                if (c == char.MinValue)
                {
                    continue;
                }

                strBuilder.Append(c);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// Gets language from first letter.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static LanguageOption GetLanguageFromFirstLetter(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return LanguageOption.Unknown;
            }

            var str = s.GetValidContent(CharOptions.Letter);

            if (str.Length == 0)
            {
                return LanguageOption.Unknown;
            }

            var firstChar = str.ToLowerInvariant()[0];
            if (firstChar >= 'a' && firstChar <= 'z')
            {
                return LanguageOption.En;
            }

            return LanguageOption.Ru;
        }

        /// <summary>
        /// Gets valid content with chars from CharOptions and specialChars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charOptions"></param>
        /// <param name="specialChars"></param>
        /// <returns></returns>
        public static string GetValidContent(this string s, CharOptions charOptions, string specialChars = null)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var rulesForChar = BuildRulesForChar(charOptions);
            if (!string.IsNullOrEmpty(specialChars))
            {
                rulesForChar.Add(specialChars.Contains);
            }

            var result = s
                .Where(c => rulesForChar.Any(func => func(c)))
                .Aggregate(string.Empty, (current, c) => current + c);

            return string.IsNullOrEmpty(specialChars) ? result : result.ReplaceTwoAndMoreCharsBySomeOne(specialChars);
        }

        /// <summary>
        /// Converts string to bool. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public static bool ToBool(this string key, bool defaultValue = false, bool raiseException = true)
        {
            if (bool.TryParse(key, out var value))
            {
                return value;
            }
            else return raiseException ? throw new ArgumentException($"{key} is not bool value.") : defaultValue;
        }

        /// <summary>
        /// Converts string to uint. When raiseException is true and is cast error raises exception otherwise returns defaultValue. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUint(this string key, uint defaultValue = 0, bool raiseException = true)
        {
            if (uint.TryParse(key, out var value))
            {
                return value;
            }
            else return raiseException ? throw new ArgumentException($"{key} is not uint value.") : defaultValue;
        }

        /// <summary>
        /// Converts string to string with value. When raiseException is true and is cast error raises exception otherwise returns defaultValue. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringWithValue(this string key, string defaultValue = "", bool raiseException = true)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return key;
            }
            else return raiseException ? throw new ArgumentException($"{key} is not null or empty.") : defaultValue;
        }

        private static List<Func<char, bool>> BuildRulesForChar(CharOptions charOptions)
        {
            var rulesForChar = new List<Func<char, bool>>();

            if ((charOptions & CharOptions.Letter) == CharOptions.Letter)
            {
                rulesForChar.Add(char.IsLetter);
            }
            if ((charOptions & CharOptions.Upper) == CharOptions.Upper)
            {
                rulesForChar.Add(char.IsUpper);
            }
            if ((charOptions & CharOptions.Lower) == CharOptions.Lower)
            {
                rulesForChar.Add(char.IsLower);
            }
            if ((charOptions & CharOptions.Digit) == CharOptions.Digit)
            {
                rulesForChar.Add(char.IsDigit);
            }
            if ((charOptions & CharOptions.Number) == CharOptions.Number)
            {
                rulesForChar.Add(char.IsNumber);
            }
            if ((charOptions & CharOptions.Separator) == CharOptions.Separator)
            {
                rulesForChar.Add(char.IsSeparator);
            }
            if ((charOptions & CharOptions.WhiteSpace) == CharOptions.WhiteSpace)
            {
                rulesForChar.Add(char.IsWhiteSpace);
            }
            if ((charOptions & CharOptions.Symbol) == CharOptions.Symbol)
            {
                rulesForChar.Add(char.IsSymbol);
            }
            if ((charOptions & CharOptions.Control) == CharOptions.Control)
            {
                rulesForChar.Add(char.IsControl);
            }
            if ((charOptions & CharOptions.Punctuation) == CharOptions.Punctuation)
            {
                rulesForChar.Add(char.IsPunctuation);
            }

            return rulesForChar;
        }
    }
}
