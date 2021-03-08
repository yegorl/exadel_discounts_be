using Exadel.CrazyPrice.Common.Models.Option;
using System;

namespace Exadel.CrazyPrice.Common.Models
{
    public class CurrentUser
    {
        public Guid Id { get; set; }

        public RoleOption Role { get; set; }
    }
}
