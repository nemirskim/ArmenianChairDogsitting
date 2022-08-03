using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArmenianChairDogsitting.API.Extensions;

public static class ControllerExtension
{
    public static string GetUri(this Controller c) =>
        $"{c.Request?.Scheme}://{c.Request?.Host.Value}{c.Request?.Path.Value}";

    public static int? GetUserId(this Controller controller)
    {
        var Claims = controller.User.Claims.ToList();
        return Convert.ToInt32(Claims[2].Value);
    }
}
