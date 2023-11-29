using Microsoft.AspNetCore.Mvc;

namespace Tarpe21ShopNoole.Controllers
{
    public class OpenWeather : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
