using Exadel.CrazyPrice.Common.Models.Option;
using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models.Request
{
    /// <summary>
    /// Represents Discount properties.
    /// </summary>
    public class UpsertDiscountRequest
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

        public List<string> Tags { get; set; }

        public LanguageOption Language { get; set; }

        public List<Translation> Translations { get; set; }
    }
}
