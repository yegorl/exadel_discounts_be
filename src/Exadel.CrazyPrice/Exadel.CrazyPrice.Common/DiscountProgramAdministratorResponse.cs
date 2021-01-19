using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common
{
    /// <summary>
    /// Gets full information for an Administrator.
    /// </summary>
    public class DiscountProgramAdministratorResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
       
        public Address Address { get; set; }

        public Company Company { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string WorkingHours { get; set; }

        public float RatingTotal { get; set; }

        public int ViewTotal { get; set; }

        public int ReservationTotal { get; set; }

        public DateTime CreateDate { get; set; }

        public Person PersonCreateDate { get; set; }

        public DateTime LastChangeDate { get; set; }

        public Person PersonLastChangeDate { get; set; }
    }
}
