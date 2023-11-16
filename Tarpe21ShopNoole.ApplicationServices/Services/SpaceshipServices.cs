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
    public class SpaceshipServices : ISpaceshipsServices
    {
        private readonly Tarpe21ShopNooleContext _context;
        private readonly IFileServices _files;
        public SpaceshipServices(Tarpe21ShopNooleContext context, IFileServices files)
        {

            _context = context;
            _files = files;
        }

        public async Task<SpaceShip> Create(SpaceshipDto dto)
        {
            SpaceShip spaceship = new SpaceShip();
            FileToDatabase file = new FileToDatabase();

            spaceship.ID = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.Description = dto.Description;
            //Dimensions = dto.Dimensions;
            spaceship.PassengerCount = dto.PassengerCount;
            spaceship.CrewCount = dto.CrewCount;
            spaceship.CargoWeight = dto.CargoWeight;
            spaceship.MaxSpeedInVaccuum = dto.MaxSpeedInVaccuum;
            spaceship.BuiltAtDate = dto.BuiltAtDate;
            spaceship.MaidenLaunch = dto.MaidenLaunch;
            spaceship.Manufacturer = dto.Manufacturer;
            spaceship.IsSpaceshipPreviouslyOwned = dto.IsSpaceshipPreviouslyOwned;
            spaceship.FullTripsCount = dto.FullTripsCount;
            spaceship.Type = dto.Type;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.FuelConsumptionPerDay = dto.FuelConsumptionPerDay;
            spaceship.MaintenanceCount = dto.MaintenanceCount;
            spaceship.LastMaintenance = dto.LastMaintenance;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, spaceship);
            }

            await _context.spaceShips.AddAsync(spaceship);
            await _context.SaveChangesAsync();
            return spaceship;
        }
        public async Task<SpaceShip> Update(SpaceshipDto dto)
        {
            var domain = new SpaceShip()
            {
                ID = dto.ID,
                Name = dto.Name,
                Description = dto.Description,
                //Dimensions = dto.Dimensions,
                PassengerCount = dto.PassengerCount,
                CrewCount = dto.CrewCount,
                CargoWeight = dto.CargoWeight,
                MaxSpeedInVaccuum = dto.MaxSpeedInVaccuum,
                BuiltAtDate = dto.BuiltAtDate,
                MaidenLaunch = dto.MaidenLaunch,
                Manufacturer = dto.Manufacturer,
                IsSpaceshipPreviouslyOwned = dto.IsSpaceshipPreviouslyOwned,
                FullTripsCount = dto.FullTripsCount,
                Type = dto.Type,
                EnginePower = dto.EnginePower,
                FuelConsumptionPerDay = dto.FuelConsumptionPerDay,
                MaintenanceCount = dto.MaintenanceCount,
                LastMaintenance = dto.LastMaintenance,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now,
            };
            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, domain);
            }
            _context.spaceShips.Update(domain);
            await _context.SaveChangesAsync(); 
            return domain;
        }
        public async Task<SpaceShip> GetUpdate(Guid id)
        {
            var result = await _context.spaceShips
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<SpaceShip> Delete(Guid id)
        {
            var spaceshipID = await _context.spaceShips
                .FirstOrDefaultAsync(x => x.ID == id);

            var images = await _context.FilesToDatabase
                .Where(x => x.SpaceshipID == id)
                .Select(y => new FileToDatabaseDto
                {
                    ID = id,
                    ImageTitle = y.ImageTitle,
                    SpaceshipID = y.SpaceshipID,
                }).ToArrayAsync();

            await _files.RemoveImageFromDatabase(images);
            _context.spaceShips.Remove(spaceshipID);
            await _context.SaveChangesAsync();

            return spaceshipID;
        }

        public async Task<SpaceShip> GetAsync(Guid id)
        {
            var result = await _context.spaceShips
                .FirstOrDefaultAsync (x => x.ID == id);
            return result;
        }
    }
}
