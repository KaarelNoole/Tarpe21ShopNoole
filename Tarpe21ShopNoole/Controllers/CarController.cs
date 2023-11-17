using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarpe21ShopNoole.ApplicationServices.Services;
using Tarpe21ShopNoole.Core.Dto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Data;
using Tarpe21ShopNoole.Models.Car;
using Tarpe21ShopNoole.Models.RealEstates;

namespace Tarpe21ShopNoole.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarServices _carService;
        private readonly Tarpe21ShopNooleContext _context;
        private readonly IFileServices _fileServices;

        public CarController(ICarServices carService, Tarpe21ShopNooleContext context, IFileServices fileServices)
        {
            _carService = carService;
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = _context.Cars
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    ID = x.ID,
                    Brand = x.Brand,
                    Model = x.Model,
                    Price = x.Price,
                    GearShiftType = x.GearShiftType,

                });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                ID = Guid.NewGuid(),
                Brand = vm.Brand,
                Model = vm.Model,
                Price = vm.Price,
                Description = vm.Description,
                GearShiftType = vm.GearShiftType,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    ID = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carService.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiVM
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.ID
                }).ToArrayAsync();
            var vm = new CarCreateUpdateViewModel();

            vm.ID = car.ID;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.Price = car.Price;
            vm.Description = car.Description;
            vm.GearShiftType = car.GearShiftType;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                ID = (Guid)vm.ID,
                Brand = vm.Brand,
                Model = vm.Model,
                Price = vm.Price,
                Description = vm.Description,
                GearShiftType = vm.GearShiftType,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    ID = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    CarId = x.CarId,
                }).ToArray()
            };
            var result = await _carService.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.CarId == id)
                .Select(y => new FileToApiVM
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.ID
                }).ToArrayAsync();

            var vm = new CarDetailsViewModel();

            vm.ID = car.ID;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.Price = car.Price;
            vm.Description = car.Description;
            vm.GearShiftType = car.GearShiftType;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);

            return View(vm);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null) 
            { 
                return NotFound();
            }

            var vm = new CarDeleteViewModel();

            vm.ID = car.ID;
            vm.Brand = car.Brand;
            vm.Model = car.Model;
            vm.Price = car.Price;
            vm.Description = car.Description;
            vm.GearShiftType = car.GearShiftType;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var car = await _carService.Delete(id);
            if (car == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiVM vm)
        {
            var dto = new FileToApiDto()
            {
                ID = vm.ImageId
            };
            var image = await _fileServices.RemoveImageFromApi(dto);
            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
