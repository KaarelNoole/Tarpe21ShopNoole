using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Xml.Linq;
using Tarpe21ShopNoole.ApplicationServices.Services;
using Tarpe21ShopNoole.Core.Dto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Data;
using Tarpe21ShopNoole.Models.Spaceship;

namespace Tarpe21ShopNoole.Controllers
{
    public class SpaceShipsController : Controller
    {
        private readonly Tarpe21ShopNooleContext _context;
        private readonly ISpaceshipsServices _SpaceshipsServices;
        private readonly IFileServices _FileServices;

        public SpaceShipsController(Tarpe21ShopNooleContext context, ISpaceshipsServices spaceshipsServices, IFileServices fileServices)
        {
            _context = context;
            _SpaceshipsServices = spaceshipsServices;
            _FileServices = fileServices;

        }
        public IActionResult Index()
        {
            var result = _context.spaceShips
                .OrderBy(x => x.CreatedAt)
                .Select(x => new SpaceshipIndexViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Type = x.Type,
                    PassengerCount = x.PassengerCount,
                    EnginePower = x.EnginePower,
                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel spaceship = new SpaceshipCreateUpdateViewModel();
            return View("CreateUpdate", spaceship);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                ID = vm.ID,
                Name = vm.Name,
                Description = vm.Description,
                PassengerCount= vm.PassengerCount,
                CrewCount= vm.CrewCount,
                CargoWeight= vm.CargoWeight,
                MaxSpeedInVaccuum = vm.MaxSpeedInVaccuum,
                BuiltAtDate = vm.BuiltAtDate,
                MaidenLaunch = vm.MaidenLaunch,
                Manufacturer = vm.Manufacturer,
                IsSpaceshipPreviouslyOwned = vm.IsSpaceshipPreviouslyOwned,
                FullTripsCount = vm.FullTripsCount,
                Type = vm.Type,
                EnginePower= vm.EnginePower,
                FuelConsumptionPerDay = vm.FuelConsumptionPerDay,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipID = x.SpaceshipID,
                }).ToArray()
            };
            var result = await _SpaceshipsServices.Create(dto);
            if (result == null)
            {
               return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _SpaceshipsServices.GetAsync(id);
            if (spaceship == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.SpaceshipID == id)
                .Select(y => new ImageViewModel
                {
                    SpaceshipID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel();

            vm.ID = spaceship.ID;
            vm.Name = spaceship.Name;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate",vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                ID = vm.ID,
                Name = vm.Name,
                Description = vm.Description,
                PassengerCount = vm.PassengerCount,
                CrewCount = vm.CrewCount,
                CargoWeight = vm.CargoWeight,
                MaxSpeedInVaccuum = vm.MaxSpeedInVaccuum,
                BuiltAtDate = vm.BuiltAtDate,
                MaidenLaunch = vm.MaidenLaunch,
                Manufacturer = vm.Manufacturer,
                IsSpaceshipPreviouslyOwned = vm.IsSpaceshipPreviouslyOwned,
                FullTripsCount = vm.FullTripsCount,
                Type = vm.Type,
                EnginePower = vm.EnginePower,
                FuelConsumptionPerDay = vm.FuelConsumptionPerDay,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipID = x.SpaceshipID,
                }).ToArray()
            };
            var result = await _SpaceshipsServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _SpaceshipsServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
                .Where(x => x.SpaceshipID == id)
                .Select(y => new ImageViewModel
                {
                    SpaceshipID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel();
            vm.ID = spaceship.ID;
            vm.Name = spaceship.Name;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {

            var spaceship = await _SpaceshipsServices.GetAsync(id);

            if (spaceship == null)
            {
                return NotFound();
            }
            var photos = await _context.FilesToDatabase
            .Where(x => x.SpaceshipID == id)
            .Select(y => new ImageViewModel
            {
                SpaceshipID = y.ID,
                ImageID = y.ID,
                ImageData = y.ImageData,
                ImageTitle = y.ImageTitle,
                Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
            }).ToArrayAsync();

            var vm = new SpaceshipDeleteViewModel();

            vm.ID = spaceship.ID;
            vm.Name = spaceship.Name;
            vm.Description = spaceship.Description;
            vm.PassengerCount = spaceship.PassengerCount;
            vm.CrewCount = spaceship.CrewCount;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.MaxSpeedInVaccuum = spaceship.MaxSpeedInVaccuum;
            vm.BuiltAtDate = spaceship.BuiltAtDate;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.Manufacturer = spaceship.Manufacturer;
            vm.IsSpaceshipPreviouslyOwned = spaceship.IsSpaceshipPreviouslyOwned;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.Type = spaceship.Type;
            vm.EnginePower = spaceship.EnginePower;
            vm.FuelConsumptionPerDay = spaceship.FuelConsumptionPerDay;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceshipID = await _SpaceshipsServices.Delete(id);
            if (spaceshipID == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                ID = file.ImageID,
            };
            var image = await _FileServices.RemoveImage(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
