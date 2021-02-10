namespace Exadel.CrazyPrice.Common.Models.SearchCriteria
{
    /// <summary>
    /// Represents min and max value for search by total rating of discount.
    /// </summary>
    public class SearchRatingTotalCriteria
    {
        public decimal? SearchRatingTotalMin { get; set; }

        public decimal? SearchRatingTotalMax { get; set; }
    }
}