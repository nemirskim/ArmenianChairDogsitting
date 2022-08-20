﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArmenianChairDogsitting.API.Extensions;

public static class ControllerExtension
{
    public static string GetUri(this Controller c) =>
        $"{c.Request?.Scheme}://{c.Request?.Host.Value}{c.Request?.Path.Value}";

    public static int? GetUserId(this Controller controller)
    {
        if (controller.HttpContext.User.Identity is not ClaimsIdentity identity)
            return null;

        if (!int.TryParse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            return null;

        return userId;
    }
    public static int? GetUserRole(this Controller controller)
    {
        if (controller.HttpContext.User.Identity is not ClaimsIdentity identity)
            return null;

        if (!int.TryParse(identity.FindFirst(ClaimTypes.Role)?.Value, out var userRole))
            return null;

        return userRole;
    }
}
