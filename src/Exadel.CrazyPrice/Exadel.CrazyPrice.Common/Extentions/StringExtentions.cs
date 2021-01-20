using System.Text.RegularExpressions;

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
            return string.IsNullOrEmpty(s) ? s : Regex.Replace(s, " {2,}", " ").Trim();
        }

    }
}
