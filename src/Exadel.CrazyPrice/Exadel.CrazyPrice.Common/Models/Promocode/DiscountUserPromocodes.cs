using System;

namespace Exadel.CrazyPrice.Common.Models.Promocode
{
    public class DiscountUserPromocodes
    {
        public Guid DiscountId { get; set; }

        public string DiscountName { get; set; }

        public Company Company { get; set; }

        public Promocode CurrentPromocode { get; set; }

        public UserPromocodes UserPromocodes { get; set; }
    }
}
