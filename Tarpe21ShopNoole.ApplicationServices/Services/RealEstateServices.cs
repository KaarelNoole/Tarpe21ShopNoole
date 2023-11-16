using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;
using Tarpe21ShopNoole.Core.Dto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Tarpe21ShopNoole.Data;
using Tarpe21ShopNoole.Data.Migrations;

namespace Tarpe21ShopNoole.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstatesServices 
    {
        private readonly Tarpe21ShopNooleContext _context;
        private readonly IFileServices _fileServices;
        public RealEstateServices(Tarpe21ShopNooleContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate realEstate = new();

            realEstate.ID = Guid.NewGuid();
            realEstate.Address = dto.Address;
            realEstate.City = dto.City;
            realEstate.Country = dto.Country;
            realEstate.County = dto.County;
            realEstate.PostalCode = dto.PostalCode;
            realEstate.PhoneNumber = dto.PhoneNumber;
            realEstate.FaxNumber = dto.FaxNumber;
            realEstate.ListingDescription = dto.ListingDescription;
            realEstate.SqueareMeters = dto.SqueareMeters;
            realEstate.BuildDate = dto.BuildDate;
            realEstate.Price = dto.Price;
            realEstate.RoomCount = dto.RoomCount;
            realEstate.FloorCount = dto.FloorCount;
            realEstate.EstateFloor = dto.EstateFloor;
            realEstate.Bathrooms = dto.Bathrooms;
            realEstate.DoesHaveParkingSpace = dto.DoesHaveParkingSpace;
            realEstate.DoesHavePowerConnection = dto.DoesHavePowerConnection;
            realEstate.DoesHaveWaterGridConnection = dto.DoesHaveWaterGridConnection;
            realEstate.Type = dto.Type;
            realEstate.IsPropertyNewDevelopent = dto.IsPropertyNewDevelopent;
            realEstate.IsPropertySold = dto.IsPropertySold;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.ModifiedAt = DateTime.Now;
            _fileServices.FilesToApi(dto, realEstate);

            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();
            return realEstate;

        }
        public async Task<RealEstate> Delete(Guid id)
        {
            var realEstateId = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.ID == id);
            _context.RealEstates.Remove(realEstateId);
            await _context.SaveChangesAsync();
            return realEstateId;
        }
        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var domain = new RealEstate()
            {
                ID = Guid.NewGuid(),
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                County = dto.County,
                PostalCode = dto.PostalCode,
                PhoneNumber = dto.PhoneNumber,
                FaxNumber = dto.FaxNumber,
                ListingDescription = dto.ListingDescription,
                SqueareMeters = dto.SqueareMeters,
                BuildDate = dto.BuildDate,
                Price = dto.Price,
                RoomCount = dto.RoomCount,
                FloorCount = dto.FloorCount,
                EstateFloor = dto.EstateFloor,
                Bathrooms = dto.Bathrooms,
                DoesHaveParkingSpace = dto.DoesHaveParkingSpace,
                DoesHavePowerConnection = dto.DoesHavePowerConnection,
                DoesHaveWaterGridConnection = dto.DoesHaveWaterGridConnection,
                Type = dto.Type,
                IsPropertyNewDevelopent = dto.IsPropertyNewDevelopent,
                IsPropertySold = dto.IsPropertySold,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
        public async Task<RealEstate> GetAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

    }
}
