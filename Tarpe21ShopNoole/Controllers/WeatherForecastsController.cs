using Microsoft.AspNetCore.Mvc;
using Tarpe21ShopNoole.Core.Dto.WeatherDto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Models.Weather;

namespace Tarpe21ShopNoole.Controllers
{
    public class WeatherForecastsController : Controller
    {
        private readonly IWeatherForecastsServices _weatherForecastServices;
        public WeatherForecastsController(IWeatherForecastsServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            WeatherViewModel vm = new WeatherViewModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult ShowWeather()
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "WeatherForecasts");
            }
            return View();
        }
        [HttpGet]
        public IActionResult City()
        {
            WeatherResultDto dto = new();
            WeatherViewModel vm = new();

            _weatherForecastServices.WeatherDetail(dto);

            vm.Date = dto.EffectiveDate;
            vm.EpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.MobileLink = dto.MobileLink;
            vm.Link = dto.Link;
            vm.Categoty = dto.Category;

            vm.Temperature.Minimum.Value = dto.TempMinValue;
            vm.Temperature.Minimum.Unit = dto.TempMinUnit;
            vm.Temperature.Minimum.UnitType = dto.TempMinUnitType;

            vm.Temperature.Maximum.Value = dto.TempMaxValue;
            vm.Temperature.Maximum.Unit = dto.TempMaxUnit;
            vm.Temperature.Maximum.UnitType = dto.TempMaxUnitType;

            vm.DayCycle.Icon = dto.DayIcon;
            vm.DayCycle.IconPhrase = dto.DayIconPhrase;
            vm.DayCycle.HasPrecipitation = dto.DayHasPrecipitation;
            vm.DayCycle.PrecipitationType = dto.DayPrecipitationType;
            vm.DayCycle.PrecipitationIntensity = dto.DayPrecipitationIntensity;

            vm.NightCycle.Icon = dto.DayIcon;
            vm.NightCycle.IconPhrase = dto.DayIconPhrase;
            vm.NightCycle.HasPrecipitation = dto.DayHasPrecipitation;
            vm.NightCycle.PrecipitationType = dto.DayPrecipitationType;
            vm.NightCycle.PrecipitationIntensity = dto.DayPrecipitationIntensity;

            return View(vm);
        }
    }
}
