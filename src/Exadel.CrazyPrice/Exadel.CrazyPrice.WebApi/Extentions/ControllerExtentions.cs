using Exadel.CrazyPrice.Common.Extentions;
using Exadel.CrazyPrice.Common.Models;
using Exadel.CrazyPrice.Common.Models.Option;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Exadel.CrazyPrice.WebApi.Extentions
{
    /// <summary>
    /// Represents methods for controllers.
    /// </summary>
    public static class ControllerExtentions
    {
        /// <summary>
        /// Gets IncomingUser from ControllerContext.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public static IncomingUser IncomingUser(this ControllerContext controllerContext) =>
            new()
            {
                Id = controllerContext.GetUserId(),
                Role = controllerContext.GetUserRole()
            };

        /// <summary>
        /// Gets a user id from ControllerContext.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public static Guid GetUserId(this ControllerContext controllerContext) =>
            controllerContext
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == "sub")
                .Select(k => k.Value)
                .FirstOrDefault()
                .ToGuid(Guid.Empty);

        /// <summary>
        /// Gets a user role from ControllerContext.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public static RoleOption GetUserRole(this ControllerContext controllerContext) =>
            controllerContext
                .HttpContext
                .User
                .Claims
                .Where(c => c.Type == "role")
                .Select(k => k.Value)
                .FirstOrDefault()
                .ToRoleOption();
    }
}
