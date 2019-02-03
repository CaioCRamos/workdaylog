using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WorkDayLog.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static Guid GetAuthenticatedUserId(this ControllerBase controller)
        {
            if (controller.User.Identity == null || !controller.User.Identity.IsAuthenticated)
                throw new Exception("Any user is authenticated at the moment");

            var userId = controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return Guid.Parse(userId.Value);
        }
    }
}