using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class ControllerExtentions
    {
        public static Guid GetUserId(this ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.User.Claims.Where(c => c.Type == "sub").Select(k => k.Value).FirstOrDefault().ToGuid(Guid.Empty);
        }
        public static string GetRole(this ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.User.Claims.Where(c => c.Type == "role").Select(k => k.Value).FirstOrDefault();
        }
    }
}
