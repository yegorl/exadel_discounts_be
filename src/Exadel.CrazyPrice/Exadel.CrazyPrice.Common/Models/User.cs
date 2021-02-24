using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the User.
    /// </summary>
    public class User : Employee
    {
        public string HashPassword { get; set; }

        public string Salt { get; set; }

        public RoleOption Roles { get; set; }

        public UserTypeOption Type { get; set; }

        public ProviderOptions Provider { get; set; }
    }
}