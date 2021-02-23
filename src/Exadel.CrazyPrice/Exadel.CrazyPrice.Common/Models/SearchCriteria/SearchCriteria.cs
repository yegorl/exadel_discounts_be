using Exadel.CrazyPrice.Common.Models.Option;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{

    /// <summary>
    /// Represents discount search criteria.
    /// </summary>
    public class SearchCriteria
    {
        /// <summary>
        /// Gets or sets a search text.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a search Discount Program option.
        /// </summary>
        public DiscountOption SearchDiscountOption { get; set; }

        /// <summary>
        /// Gets or sets a search Country.
        /// </summary>
        public string SearchAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets a search City.
        /// </summary>
        public string SearchAddressCity { get; set; }

        /// <summary>
        /// Gets or sets a sort field for search.
        /// </summary>
        public SortFieldOption SearchSortFieldOption { get; set; }

        /// <summary>
        /// Gets or sets a sort option for search.
        /// </summary>
        public SortOption SearchSortOption { get; set; }

        /// <summary>
        /// Gets or sets a number page.
        /// </summary>
        public int SearchPaginationPageNumber { get; set; }

        /// <summary>
        /// Gets or sets a Count Element per Page.
        /// </summary>
        public int SearchPaginationCountElementPerPage { get; set; }

        /// <summary>
        /// Gets or sets a Language.
        /// </summary>
        public LanguageOption SearchLanguage { get; set; }

        /// <summary>
        /// Gets or sets advanced property for search
        /// </summary>
        public SearchAdvancedCriteria SearchAdvanced { get; set; }

        /// <summary>
        /// Gets or sets a search User Id.
        /// </summary>
        [JsonIgnore]
        public IncomingUser IncomingUser { get; set; }

        /// <summary>
        /// Gets or sets a search all when SearchShowDeleted is true otherwise search when deleted is false.
        /// </summary>
        [JsonIgnore]
        public bool SearchShowDeleted { get; set; }
    }
}
