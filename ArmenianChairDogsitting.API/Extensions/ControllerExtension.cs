using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Extensions
{
    public static class ControllerExtension
    {
        public static string GetUri(this Controller c) =>
            $"{c.Request.Scheme}://{c.Request.Host.Value}{c.Request.Path.Value}";
    }
}
