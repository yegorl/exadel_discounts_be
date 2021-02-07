using System.ComponentModel;
using System.Text.Json.Serialization;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    public class Employee: User
    {
        [JsonIgnore, DefaultValue(null)]
        public override string HashPassword => null;

        [JsonIgnore, DefaultValue(null)]
        public override string Salt => null;

        [JsonIgnore, DefaultValue(RoleOption.Unknown)]
        public override RoleOption Roles => RoleOption.Unknown;

        public override bool IsEmpty => Equals(new Employee());
    }
}