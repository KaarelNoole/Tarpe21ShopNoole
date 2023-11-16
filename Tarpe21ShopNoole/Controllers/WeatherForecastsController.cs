using Microsoft.AspNetCore.Mvc;

namespace Tarpe21ShopNoole.Controllers
{
    public class WeatherForecastsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
