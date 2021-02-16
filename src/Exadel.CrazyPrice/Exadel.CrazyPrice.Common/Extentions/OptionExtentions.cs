using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exadel.CrazyPrice.Common.Extentions
{
    /// <summary>
    /// Determines methods 
    /// </summary>
    public static class OptionExtentions
    {
        /// <summary>
        /// Gets the string value in lowercase from LanguageOption entity.
        /// </summary>
        /// <param name="languageOption"></param>
        /// <param name="fromLookupLanguage"></param>
        /// <returns></returns>
        public static string ToStringLookup(this LanguageOption languageOption, bool fromLookupLanguage = true)
        {
            return fromLookupLanguage ? LookupLanguage[languageOption] : languageOption.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Gets the LanguageOption value from string.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="raiseException"></param>
        /// <returns></returns>
        public static LanguageOption ToLanguageOption(this string key, LanguageOption defaultValue = LanguageOption.En, bool raiseException = false)
        {
            var lookupLanguage = LookupLanguage;
            key = key.ToLowerInvariant();

            if (lookupLanguage.Values.Contains(key))
            {
                var pair = lookupLanguage.First(kvp => kvp.Value == key);
                return pair.Key;
            }

            if (Enum.TryParse(key, true, out LanguageOption value))
            {
                return value;
            }
            else
            {
                return raiseException ? throw new ArgumentException($"{key} is not LanguageOption value.") : defaultValue;
            }
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
                return LanguageOption.En;
            }

            var str = s.GetValidContent(CharOptions.Letter);

            if (str.Length == 0)
            {
                return LanguageOption.En;
            }

            var firstChar = str.ToLowerInvariant()[0];
            if (firstChar >= 'a' && firstChar <= 'z')
            {
                return LanguageOption.En;
            }

            return LanguageOption.Ru;
        }

        /// <summary>
        /// Gets sort fields when fromLookupSortField is true SortFieldOption converts from lookup list otherwise from enum type.
        /// </summary>
        /// <param name="sortFieldOption"></param>
        /// <param name="fromLookupSortField"></param>
        /// <returns></returns>
        public static string ToStringLookup(this SortFieldOption sortFieldOption, bool fromLookupSortField = true)
        {
            return fromLookupSortField ? LookupSortField[sortFieldOption] : sortFieldOption.ToString().ToLowerInvariant();
        }

        /// <summary>
        /// Gets value with translations prefix with language when onlyPrefix is true otherwise gets only prefix.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="languageOption"></param>
        /// <param name="onlyPrefix"></param>
        /// <returns></returns>
        public static string GetWithTranslationsPrefix(this string value, LanguageOption languageOption,
            bool onlyPrefix = false) =>
            languageOption != LanguageOption.Ru
                ? (onlyPrefix ? "translations." : "translations." + value)
                : (onlyPrefix ? string.Empty : value);

        private static Dictionary<LanguageOption, string> LookupLanguage => new()
        {
            { LanguageOption.En, "english" },
            { LanguageOption.Ru, "russian" }
        };

        private static Dictionary<SortFieldOption, string> LookupSortField => new()
        {
            { SortFieldOption.NameDiscount, "name" },
            { SortFieldOption.CompanyName, "company.name" },
            { SortFieldOption.AmountOfDiscount, "amountOfDiscount" },
            { SortFieldOption.RatingDiscount, "ratingTotal" },
            { SortFieldOption.DateStart, "startDate" },
            { SortFieldOption.DateEnd, "endDate" },
        };
    }
}
