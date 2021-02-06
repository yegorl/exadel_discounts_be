using System;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    public class User
    {
        public virtual Guid Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Mail { get; set; }

        public virtual string HashPassword { get; set; }

        public virtual string Salt { get; set; }

        public virtual RoleOption Roles { get; set; }
    }
}
