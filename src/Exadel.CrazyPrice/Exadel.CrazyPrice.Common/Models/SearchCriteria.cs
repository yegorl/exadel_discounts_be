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
    }
}
