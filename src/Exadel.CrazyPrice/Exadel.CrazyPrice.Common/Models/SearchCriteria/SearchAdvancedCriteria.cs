namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    /// <summary>
    /// Represents optional criteria for search advanced.
    /// </summary>
    public class SearchAdvancedCriteria
    {
        public string CompanyName { get; set; }

        public SearchDateCriteria SearchDate { get; set; }

        public SearchAmountOfDiscountCriteria SearchAmountOfDiscount { get; set; }

        public SearchRatingTotalCriteria SearchRatingTotal { get; set; }
    }
}
