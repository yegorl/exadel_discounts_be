using System.Collections.Generic;
using System.Text.Json.Serialization;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the User.
    /// </summary>
    public class User : UserInfo
    {
        public string HashPassword { get; set; }

        public string Salt { get; set; }

        [JsonIgnore]
        public RoleOption Roles { get; set; }

        public List<ExternalUser> ExternalUsers { get; set; }
    }
}