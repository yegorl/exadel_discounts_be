using System;

namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    /// <summary>
    /// Represents min and max date for search by date of discount.
    /// </summary>
    public class SearchDateCriteria
    {
        public DateTime? SearchStartDate { get; set; }

        public DateTime? SearchEndDate { get; set; }
    }
}
