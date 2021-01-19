using System;

namespace Exadel.CrazyPrice.Common
{
    /// <summary>
    /// Gets short information for an Administrator
    /// </summary>
    public class ShortDiscountProgramAdministratorResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public Company Company { get; set; }

        public float RatingTotal { get; set; }

        public int ViewTotal { get; set; }

        public int ReservationTotal { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }
    }
}
