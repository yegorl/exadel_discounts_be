using System.Text.RegularExpressions;

namespace Exadel.CrazyPrice.Common.Ensure
{
    /// <summary>
    /// Represents correcting string.
    /// </summary>
    public static class StringExtention
    {

        /// <summary>
        /// Removes 2 and more space. Trim.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReplaceSpace(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return Regex.Replace(s, " {2,}", " ").Trim();
        }

    }
}
