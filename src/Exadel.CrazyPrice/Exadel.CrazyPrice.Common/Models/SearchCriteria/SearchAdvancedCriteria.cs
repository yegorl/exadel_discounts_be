namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    public class SearchAdvancedCriteria
    {
        public string CompanyName { get; set; }

        public SearchDateCriteria SearchStartDate { get; set; }

        public SearchDateCriteria SearchEndDate { get; set; }

        public SearchAmountOfDiscountCriteria SearchAmountOfDiscount { get; set; }

        public SearchRatingTotalCriteria SearchRatingTotal { get; set; }
    }
}
