using Exadel.CrazyPrice.Common.Models.Option;
using System;

namespace Exadel.CrazyPrice.Common.Models
{
    public class IncomingUser
    {
        public Guid Id { get; set; }

        public RoleOption Role { get; set; }
    }
}
