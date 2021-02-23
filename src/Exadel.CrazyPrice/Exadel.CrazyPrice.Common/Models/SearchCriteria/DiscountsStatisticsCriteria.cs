using System;

namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    public class DiscountsStatisticsCriteria
    {
        /// <summary>
        /// Gets or sets a search CreateStartDate.
        /// </summary>
        public DateTime? CreateStartDate { get; set; }

        /// <summary>
        /// Gets or sets a search CreateEndDate.
        /// </summary>
        public DateTime? CreateEndDate { get; set; }

        /// <summary>
        /// Gets or sets a search Country.
        /// </summary>
        public string SearchAddressCountry { get; set; }

        /// <summary>
        /// Gets or sets a search City.
        /// </summary>
        public string SearchAddressCity { get; set; }
    }
}
