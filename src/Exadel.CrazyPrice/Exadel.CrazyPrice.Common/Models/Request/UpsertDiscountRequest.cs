using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common.Models.Request
{
    /// <summary>
    /// Gets/sets information for a Moderator
    /// </summary>
    public class UpsertDiscountRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
       
        public Address Address { get; set; }

        public Company Company { get; set; }

        public List<string> Tags { get; set; }

        public string WorkingHours { get; set; }
    }
}
