using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models.Response
{
    /// <summary>
    /// Gets full information.
    /// </summary>
    public class DiscountResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? AmountOfDiscount { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartDate { get; set; }
       
        public Address Address { get; set; }

        public Company Company { get; set; }

        public string WorkingHours { get; set; }

        public List<string> Tags { get; set; }

        public decimal? RatingTotal { get; set; }

        public int? ViewTotal { get; set; }

        public List<string> ViewPersonsId { get; set; }

        public int? ReservationTotal { get; set; }

        public List<string> ReservationPersonsId { get; set; }

        public DateTime CreateDate { get; set; }

        public Person PersonCreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public Person PersonLastChangeDate { get; set; }
    }
}
