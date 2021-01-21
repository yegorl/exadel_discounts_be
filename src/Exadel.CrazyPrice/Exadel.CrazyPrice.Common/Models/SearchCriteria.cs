using System;

namespace Exadel.CrazyPrice.Common.Models
{
    public class SearchCriteria
    {      
        /// <summary>
        /// Gets or sets a search text.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a search Country.
        /// </summary>
        public string SearchCountry { get; set; }

        /// <summary>
        /// Gets or sets a search City.
        /// </summary>
        public string SearchCity { get; set; }

        /// <summary>
        /// Gets or sets a search Discount Program option.
        /// </summary>
        public DiscountOption SearchDiscountOption { get; set; }

        /// <summary>
        /// Gets or sets a search Person Id.
        /// </summary>
        public Guid SearchPersonId { get; set; }

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
        public int SearchPageNumber { get; set; }

        /// <summary>
        /// Gets or sets a Count Element per Page.
        /// </summary>
        public int SearchCountElementPerPage { get; set; }
    }
}
