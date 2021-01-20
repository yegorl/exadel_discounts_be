namespace Exadel.CrazyPrice.Common.Models
{
    public class SearchCriteria
    {      
        /// <summary>
        /// Gets or sets a search text.
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a City.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a Discount Program option.
        /// </summary>
        public DiscountOption DiscountOption { get; set; }
    }
}
