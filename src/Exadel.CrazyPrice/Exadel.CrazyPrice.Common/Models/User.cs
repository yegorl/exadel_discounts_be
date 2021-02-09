using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    public class User : Employee
    {
        public string HashPassword { get; set; }

        public string Salt { get; set; }

        public RoleOption Roles { get; set; }
    }
}
