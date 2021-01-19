using System;
using System.Collections.Generic;

namespace Exadel.CrazyPrice.Common
{
    /// <summary>
    /// Gets/sets information for a Moderator
    /// </summary>
    public class NewDiscountProgramModerator
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
    }
}
