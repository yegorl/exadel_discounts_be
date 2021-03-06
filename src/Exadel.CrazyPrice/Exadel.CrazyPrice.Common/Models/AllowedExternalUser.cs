﻿using System;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the ExternalUser.
    /// </summary>
    public class AllowedExternalUser
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public RoleOption Roles { get; set; }
    }
}
