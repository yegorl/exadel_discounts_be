namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    /// <summary>
    /// Represents min and max value for search by amount of discount.
    /// </summary>
    public class SearchAmountOfDiscountCriteria
    {
        public decimal? SearchAmountOfDiscountMin { get; set; }

        public decimal? SearchAmountOfDiscountMax { get; set; }
    }
}