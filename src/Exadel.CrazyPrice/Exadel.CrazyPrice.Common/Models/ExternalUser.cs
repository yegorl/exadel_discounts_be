﻿using System.Collections.Generic;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.Common.Models
{
    /// <summary>
    /// Represents the ExternalUser.
    /// </summary>
    public class ExternalUser
    {
        public string Identifier { get; set; }

        public ProviderOption Provider { get; set; }
    }
}
