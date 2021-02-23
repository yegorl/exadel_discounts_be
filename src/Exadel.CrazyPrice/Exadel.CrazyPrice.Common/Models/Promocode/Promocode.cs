using System;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Common.Models.Promocode
{
    /// <summary>
    /// Represent the Promocode.
    /// </summary>
    public class Promocode
    {
        public Guid Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EndDate { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }

        public string PromocodeValue { get; set; }

        public bool Expired => EndDate != null && EndDate < DateTime.UtcNow;


    }
}
