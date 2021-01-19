using System;

namespace Exadel.CrazyPrice.Common
{
    /// <summary>
    /// Gets short information for a Person
    /// </summary>
    public class ShortDiscountProgram
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public Company Company { get; set; }

        public float RatingTotal { get; set; }
    }
}
