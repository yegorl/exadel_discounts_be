using Exadel.CrazyPrice.Common.Extentions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Exadel.CrazyPrice.Common.Models.Option;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    public static class ControllerExtentions
    {
        public static Guid GetUserId(this ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.User.Claims.Where(c => c.Type == "sub").Select(k => k.Value).FirstOrDefault().ToGuid(Guid.Empty);
        }
        public static RoleOption GetRole(this ControllerContext controllerContext)
        {
            var roleAsString = controllerContext.HttpContext.User.Claims.Where(c => c.Type == "role")
                .Select(k => k.Value).FirstOrDefault();
            object obj;
            if (Enum.TryParse(typeof(RoleOption), roleAsString, out obj))
            {
                return (RoleOption) obj;
            }
            else return RoleOption.Unknown;
        }
    }
}
