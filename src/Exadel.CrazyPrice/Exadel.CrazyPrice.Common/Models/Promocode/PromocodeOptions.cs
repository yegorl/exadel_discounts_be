namespace Exadel.CrazyPrice.Common.Models.Promocode
{
    /// <summary>
    /// Represents the PromocodeOptions.
    /// </summary>
    public class PromocodeOptions
    {
        public int? CountActivePromocodePerUser { get; set; }

        public int? DaysDurationPromocode { get; set; }

        public int? CountSymbolsPromocode { get; set; }

        public int? TimeLimitAddingInSeconds { get; set; }
    }
}
