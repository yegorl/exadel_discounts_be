namespace Exadel.CrazyPrice.Common.Models.Promocode
{
    /// <summary>
    /// Represents the PromocodeOptions.
    /// </summary>
    public class PromocodeOptions
    {
        public bool? EnabledPromocodes { get; set; }

        public int? CountActivePromocodePerUser { get; set; }

        public int? DaysDurationPromocode { get; set; }

        public int? CountSymbolsPromocode { get; set; }

        public int? TimeLimitAddingInSeconds { get; set; }
    }
}
