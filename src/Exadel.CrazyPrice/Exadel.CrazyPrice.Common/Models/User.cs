using System;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Mail { get; set; }

        public string HashPassword { get; set; }

        public string Salt { get; set; }

        public RoleOption Roles { get; set; }
    }
}
