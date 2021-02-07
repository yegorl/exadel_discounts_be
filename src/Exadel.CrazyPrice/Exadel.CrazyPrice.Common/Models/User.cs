using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    public class User : Employee
    {
        public string HashPassword { get; init; }

        public string Salt { get; init; }

        public RoleOption Roles { get; init; }
    }
}
