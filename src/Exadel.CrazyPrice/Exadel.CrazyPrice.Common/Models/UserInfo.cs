using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represented User info. 
    /// </summary>
    public class UserInfo : Employee
    {
        public string PhotoUrl { get; set; }

        public LanguageOption Language { get; set; }
    }
}