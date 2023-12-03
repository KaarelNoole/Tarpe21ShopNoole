using Microsoft.AspNetCore.Mvc;
using Tarpe21ShopNoole.ApplicationServices.Services;
using Tarpe21ShopNoole.Core.Dto.OpenWeatherDto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Models.OpenWeather;

namespace Tarpe21ShopNoole.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IWeatherForecastsServices _openWeatherForecastServices;
        SearchCityViewModel vm = new SearchCityViewModel();

        public OpenWeatherController(IWeatherForecastsServices openWeathersServices)
        {
            _openWeatherForecastServices = openWeathersServices;
        }
        public IActionResult Index()
        {
            SearchCityViewModel vm = new SearchCityViewModel();

            return View();
        }
        [HttpPost]
        public IActionResult ShowWeather()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City","OpenWeathers");
            }

            return View();
        }
        [HttpPost]
        public IActionResult SearchCity(SearchCityViewModel searchCityViewModel)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                return RedirectToAction("City", "OpenWeathers", new { city = searchCityViewModel.CityName });
            }

        }
        [HttpGet]
        public IActionResult City(string city)
        {
            OpenWeatherResultDto dto = new();
            CityResultViewModel vm = new CityResultViewModel();
            dto.City = city;
            _openWeatherForecastServices.OpenWeatherDetail(dto);
            vm.City = city;
            vm.Timezone = dto.Timezone;
            vm.Name = dto.Name;
            vm.Lon = dto.Lon;
            vm.Lat = dto.Lat;
            vm.Temperature = dto.Temperature;
            vm.Feels_like = dto.Feels_like;
            vm.Pressure = dto.Pressure;
            vm.Humidity = dto.Humidity;
            vm.Main = dto.Main;
            vm.Description = dto.Description;
            vm.Speed = dto.Speed;
            return View(vm);
        }
    }
}
