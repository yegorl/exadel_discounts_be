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
        public static DateTime GetUtcDateTime(this string stringDate)
        {
            if (stringDate.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(stringDate));
            }

            return DateTime.ParseExact(stringDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture).ToUniversalTime();
        }

        /// <summary>
        /// Gets true if string is valid WorkingDays otherwise false.
        /// </summary>
        /// <param name="stringWorkingDays"></param>
        /// <returns></returns>
        public static bool IsValidWorkingDays(this string stringWorkingDays)
        {
            if (stringWorkingDays.IsNullOrEmpty() || stringWorkingDays.Length != 7)
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
            if (chars.IsNullOrEmpty())
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
            if (s.IsNullOrEmpty())
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
        /// Gets valid content with chars from CharOptions and specialChars.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charOptions"></param>
        /// <param name="specialChars"></param>
        /// <returns></returns>
        public static string GetValidContent(this string s, CharOptions charOptions, string specialChars = null)
        {
            if (s.IsNullOrEmpty())
            {
                return string.Empty;
            }

            var rulesForChar = BuildRulesForChar(charOptions);
            if (!specialChars.IsNullOrEmpty())
            {
                rulesForChar.Add(specialChars.Contains);
            }

            var result = s
                .Where(c => rulesForChar.Any(func => func(c)))
                .Aggregate(string.Empty, (current, c) => current + c);

            return specialChars.IsNullOrEmpty() ? result : result.ReplaceTwoAndMoreCharsBySomeOne(specialChars);
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
            else
            {
                return raiseException ? throw new ArgumentException($"'{key}' is not bool value.") : defaultValue;
            }
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
            else
            {
                return raiseException ? throw new ArgumentException($"'{key}' is not uint value.") : defaultValue;
            }
        }

        /// <summary>
        /// Converts string to string with value. When raiseException is true and is cast error raises exception otherwise returns defaultValue. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="raiseException"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToStringWithValue(this string key, string name, string defaultValue = "", bool raiseException = true)
        {
            if (!key.IsNullOrEmpty())
            {
                return key;
            }
            else
            {
                return raiseException ? throw new ArgumentException($"'{name}' is null or empty.") : defaultValue;
            }
        }

        /// <summary>
        /// Converts string to Guid. When raiseException is true and is cast error raises exception otherwise returns defaultValue.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string key, Guid defaultValue, bool raiseException = true)
        {
            if (Guid.TryParse(key, out var value))
            {
                return value;
            }
            else
            {
                return raiseException ? throw new ArgumentException($"'{key}' is not Guid value.") : defaultValue;
            }
        }

        /// <summary>
        /// Gets string newValue from value when keys exist in possibleValues.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string OverLoadString(this string value, string newValue, string[] possibleValues, params string[] keys) =>
            KeysExist(possibleValues, keys) ? newValue : value;

        /// <summary>
        /// Gets bool newValue from value when keys exist in possibleValues. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool OverLoadBool(this bool value, bool newValue, string[] possibleValues, params string[] keys) =>
            KeysExist(possibleValues, keys) && newValue;

        /// <summary>
        /// Gets uint newValue from value when keys exist in possibleValues. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="newValue"></param>
        /// <param name="possibleValues"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static uint OverLoadUint(this uint value, uint newValue, string[] possibleValues, params string[] keys) =>
            KeysExist(possibleValues, keys) ? newValue : value;

        /// <summary>
        /// Gets true when last characters in value are symbols otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbols"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool IsLast(this string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase, params string[] symbols) =>
            !value.IsNullOrEmpty() && symbols.Any(s => value.LastIndexOf(s, comparison) == value.Length - 1);

        /// <summary>
        /// Gets true when value is Null or Empty or last characters in value are symbols otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbols"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrLast(this string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase, params string[] symbols) =>
            value.IsNullOrEmpty() || value.IsLast(comparison, symbols);

        /// <summary>
        /// Gets true when value is Null or Empty otherwise false.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value) =>
            string.IsNullOrEmpty(value);

        /// <summary>
        /// Gets true when strings is Empty otherwise false.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string[] strings) =>
            strings == null || strings.Length == 0;

        /// <summary>
        /// Gets true when strings is Empty otherwise false.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this List<string> strings) =>
            strings == null || strings.Count == 0;

        /// <summary>
        /// Gets true when dictionary is Empty otherwise false.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this Dictionary<string, string> dictionary) =>
            dictionary.Count == 0;

        /// <summary>
        /// Converts string to Uri. When raiseException is true and is cast error raises exception otherwise returns <c>null</c>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="uriKind"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public static Uri ToUri(this string key, UriKind uriKind = UriKind.Absolute, bool raiseException = true)
        {
            if (Uri.TryCreate(key, uriKind, out var value))
            {
                return value;
            }
            else
            {
                return raiseException ? throw new ArgumentException($"'{key}' is not Uri value.") : null;
            }
        }
        /// <summary>
        /// Gets a NewPromocodeValue contains letters and digit. CountSymbol must have symbols great then 4 and less than 25.
        /// </summary>
        /// <param name="countSymbol"></param>
        /// <returns></returns>
        public static string NewPromocodeValue(int countSymbol = 5) =>
            Guid.NewGuid().ToString().Replace("-", "")
                .Substring(0, countSymbol < 4 ? 4 : countSymbol >= 25 ? 25 : countSymbol)
                .ToUpperInvariant();

        private static bool KeysExist(string[] possibleValues, params string[] keys) =>
            keys.Any(k => string.Join("(|)", possibleValues).ToLowerInvariant().Split("(|)").Contains(k.ToLowerInvariant()));

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
