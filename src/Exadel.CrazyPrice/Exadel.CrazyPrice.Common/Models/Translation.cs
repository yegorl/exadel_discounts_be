using Exadel.CrazyPrice.Common.Models.Option;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the translation.
    /// </summary>
    public class Translation
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TranslationAddress Address { get; set; }

        public TranslationCompany Company { get; set; }

        public List<string> Tags { get; set; }

        public LanguageOption Language { get; set; }
    }
}