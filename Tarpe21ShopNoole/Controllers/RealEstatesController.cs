using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;
using Tarpe21ShopNoole.ApplicationServices.Services;
using Tarpe21ShopNoole.Core.Dto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Data;
using Tarpe21ShopNoole.Models.RealEstate;
using Tarpe21ShopNoole.Models.RealEstates;
using Tarpe21ShopNoole.Models.Spaceship;

namespace Tarpe21ShopNoole.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstatesServices;
        private readonly Tarpe21ShopNooleContext _context;
        private readonly IFileServices _fileServices;
        public RealEstatesController(IRealEstatesServices realEstatesServices, Tarpe21ShopNooleContext context, IFileServices fileServices)
        {
            _realEstatesServices = realEstatesServices;
            _context = context;
            _fileServices = fileServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = _context.RealEstates
               .OrderByDescending(x => x.CreatedAt)
               .Select(x => new RealEstateIndexViewModel
               {
                   ID = x.ID,
                   Address = x.Address,
                   City = x.City,
                   Country = x.Country,
                   SqueareMeters = x.SqueareMeters,
                   Price = x.Price,
                   IsPropertySold = x.IsPropertySold,
               });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();
            return View("CreateUpdate",vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                ID = Guid.NewGuid(),
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                County = vm.County,
                SqueareMeters = vm.SqueareMeters,
                Price= vm.Price,
                PostalCode = vm.PostalCode,
                PhoneNumber = vm.PhoneNumber,
                FaxNumber = vm.FaxNumber,
                ListingDescription = vm.ListingDescription,
                BuildDate = vm.BuildDate,
                RoomCount = vm.RoomCount,
                FloorCount = vm.FloorCount,
                EstateFloor = vm.EstateFloor,
                Bathrooms = vm.Bathrooms,
                Bedrooms = vm.Bedrooms,
                DoesHaveParkingSpace = vm.DoesHaveParkingSpace,
                DoesHavePowerConnection = vm.DoesHavePowerConnection,
                DoesHaveWaterGridConnection = vm.DoesHaveWaterGridConnection,
                Type= vm.Type,
                IsPropertyNewDevelopent = vm.IsPropertyNewDevelopent,
                IsPropertySold = vm.IsPropertySold,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    ID = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    RealEstateId = x.RealEstateId,
                }).ToArray()
            };
            var result = await _realEstatesServices.Create(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.ID
                }).ToArrayAsync();
            var vm = new RealEstateCreateUpdateViewModel();

            vm.ID = realEstate.ID;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SqueareMeters = realEstate.SqueareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerConnection = realEstate.DoesHavePowerConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopent = realEstate.IsPropertyNewDevelopent;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.CreatedAt = DateTime.Now;
            vm.ModifiedAt = DateTime.Now;
            vm.FileToApiViewModels.AddRange(images);

            return View("CreateUpdate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                ID = Guid.NewGuid(),
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                County = vm.County,
                SqueareMeters = vm.SqueareMeters,
                Price = vm.Price,
                PostalCode = vm.PostalCode,
                PhoneNumber = vm.PhoneNumber,
                FaxNumber = vm.FaxNumber,
                ListingDescription = vm.ListingDescription,
                BuildDate = vm.BuildDate,
                RoomCount = vm.RoomCount,
                FloorCount = vm.FloorCount,
                EstateFloor = vm.EstateFloor,
                Bathrooms = vm.Bathrooms,
                Bedrooms = vm.Bedrooms,
                DoesHaveParkingSpace = vm.DoesHaveParkingSpace,
                DoesHavePowerConnection = vm.DoesHavePowerConnection,
                DoesHaveWaterGridConnection = vm.DoesHaveWaterGridConnection,
                Type = vm.Type,
                IsPropertyNewDevelopent = vm.IsPropertyNewDevelopent,
                IsPropertySold = vm.IsPropertySold,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                .Select(x => new FileToApiDto
                {
                    ID = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    RealEstateId = x.RealEstateId,
                })
            };
            var result = await _realEstatesServices.Update(dto);
            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.ID
                }).ToArrayAsync();

            var vm = new RealEstateDetailsViewModel();

            vm.ID = realEstate.ID;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SqueareMeters = realEstate.SqueareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerConnection = realEstate.DoesHavePowerConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopent = realEstate.IsPropertyNewDevelopent;
            vm.IsPropertySold = realEstate.IsPropertySold;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images); 

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstatesServices.GetAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            var vm = new RealEstateDeleteViewModel();

            vm.ID = realEstate.ID;
            vm.Address = realEstate.Address;
            vm.City = realEstate.City;
            vm.Country = realEstate.Country;
            vm.County = realEstate.County;
            vm.SqueareMeters = realEstate.SqueareMeters;
            vm.Price = realEstate.Price;
            vm.PostalCode = realEstate.PostalCode;
            vm.PhoneNumber = realEstate.PhoneNumber;
            vm.FaxNumber = realEstate.FaxNumber;
            vm.ListingDescription = realEstate.ListingDescription;
            vm.BuildDate = realEstate.BuildDate;
            vm.RoomCount = realEstate.RoomCount;
            vm.FloorCount = realEstate.FloorCount;
            vm.EstateFloor = realEstate.EstateFloor;
            vm.Bathrooms = realEstate.Bathrooms;
            vm.Bedrooms = realEstate.Bedrooms;
            vm.DoesHaveParkingSpace = realEstate.DoesHaveParkingSpace;
            vm.DoesHavePowerConnection = realEstate.DoesHavePowerConnection;
            vm.DoesHaveWaterGridConnection = realEstate.DoesHaveWaterGridConnection;
            vm.Type = realEstate.Type;
            vm.IsPropertyNewDevelopent = realEstate.IsPropertyNewDevelopent;
            vm.IsPropertySold = realEstate.IsPropertySold;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstatesServices.Delete(id);
            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
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
