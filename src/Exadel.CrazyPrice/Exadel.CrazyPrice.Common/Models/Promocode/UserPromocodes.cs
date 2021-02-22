using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Exadel.CrazyPrice.Common.Models.Promocode
{
    /// <summary>
    /// Represents the UserPromocodes.
    /// </summary>
    public class UserPromocodes
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        public List<Promocode> Promocodes { get; set; }
    }
}
