using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
