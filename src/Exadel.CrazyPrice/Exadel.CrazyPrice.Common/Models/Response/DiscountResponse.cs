using Exadel.CrazyPrice.Common.Models.Promocode;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models.Response
{
    /// <summary>
    /// Represents the DiscountResponse.
    /// </summary>
    public class DiscountResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? AmountOfDiscount { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Address Address { get; set; }

        public Company Company { get; set; }

        public string WorkingDaysOfTheWeek { get; set; }

        public string PictureUrl { get; set; }

        public List<UserPromocodes> UserPromocodes { get; set; }

        public List<string> Tags { get; set; }

        public decimal? RatingTotal { get; set; }

        public int? ViewsTotal { get; set; }

        public int? SubscriptionsTotal { get; set; }

        public int? UsersSubscriptionTotal { get; set; }

        public DateTime? CreateDate { get; set; }

        public Employee UserCreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public Employee UserLastChangeDate { get; set; }

        public bool Deleted { get; set; }
    }
}
