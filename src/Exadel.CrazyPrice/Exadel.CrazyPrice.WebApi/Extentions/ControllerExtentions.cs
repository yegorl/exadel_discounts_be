﻿using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class ControllerExtentions
    {
        public static Guid GetUserId(this ControllerContext controllerContext) =>
            controllerContext
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == "sub")
                .Select(k => k.Value)
                .FirstOrDefault()
                .ToGuid(Guid.Empty);
    }
}
