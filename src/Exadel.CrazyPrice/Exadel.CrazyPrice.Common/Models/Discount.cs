using Exadel.CrazyPrice.Common.Models.Promocode;
using Exadel.CrazyPrice.Common.Models.Request;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the discount.
    /// </summary>
    public class Discount : UpsertDiscountRequest
    {
        public decimal? RatingTotal { get; set; }

        public int? ViewsTotal { get; set; }

        public int? SubscriptionsTotal { get; set; }

        public int? UsersSubscriptionTotal { get; set; }

        public List<string> FavoritesUsersId { get; set; }

        public List<UserPromocodes> UsersPromocodes { get; set; }

        public DateTime? CreateDate { get; set; }

        public User UserCreateDate { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public User UserLastChangeDate { get; set; }

        public bool Deleted { get; set; }
    }
}