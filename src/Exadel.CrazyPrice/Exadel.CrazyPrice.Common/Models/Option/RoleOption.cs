using System;

namespace Exadel.CrazyPrice.Common.Models.Option
{
    [Flags]
    public enum RoleOption
    {
        Employee = 1,
        Moderator = 2,
        Admin = 4
    }
}
