using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    public class DiscountsStatisticCriteria
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
