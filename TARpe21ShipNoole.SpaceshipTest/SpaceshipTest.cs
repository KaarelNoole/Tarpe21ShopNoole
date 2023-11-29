﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;
using Tarpe21ShopNoole.Core.Dto;
using Tarpe21ShopNoole.Core.ServiceInterface;
using Xunit;

namespace TARpe21ShipNoole.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            string guid = Guid.NewGuid().ToString();

            SpaceshipDto spaceship = new SpaceshipDto();

            spaceship.ID = Guid.NewGuid();
            spaceship.Name = "Testname";
            spaceship.Description = "Test description";
            spaceship.PassengerCount = 4;
            spaceship.CrewCount = 4;
            spaceship.CargoWeight = 3000;
            spaceship.MaxSpeedInVaccuum = 200;
            spaceship.BuiltAtDate = DateTime.Now;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.Manufacturer = "Test manufacturer";
            spaceship.IsSpaceshipPreviouslyOwned = true;
            spaceship.FullTripsCount = 1;
            spaceship.Type = "Test Type";
            spaceship.EnginePower = 9001;
            spaceship.FuelConsumptionPerDay = 4000;
            spaceship.MaintenanceCount = 0;
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            var result = await Svc<ISpaceshipsServices>().Create(spaceship);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var SpaceShip = await Svc<ISpaceshipsServices>().Create(dto);

            var result = await Svc<ISpaceshipsServices>().Delete((Guid)SpaceShip.ID);

            Assert.Equal(result, SpaceShip);
        }

        [Fact]
        public async Task Should_UpdateSpaceship_WhenUpdateData()
        {
            var guid = new Guid("b3fa691d-12e0-465b-b95e-b31566a5b7c1");

            SpaceShip spaceship = new SpaceShip();
            SpaceshipDto dto = MockSpaceshipData();

            spaceship.ID = Guid.Parse("b3fa691d-12e0-465b-b95e-b31566a5b7c1");
            spaceship.Name = "Edit Testname";
            spaceship.Description = "Test description";
            spaceship.PassengerCount = 400;
            spaceship.CrewCount = 4;
            spaceship.CargoWeight = 3000;
            spaceship.MaxSpeedInVaccuum = 200;
            spaceship.BuiltAtDate = DateTime.Now;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.Manufacturer = "Test manufacturer";
            spaceship.IsSpaceshipPreviouslyOwned = true;
            spaceship.FullTripsCount = 1;
            spaceship.Type = "Test Type";
            spaceship.EnginePower = 9001;
            spaceship.FuelConsumptionPerDay = 4000;
            spaceship.MaintenanceCount = 0;
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            await Svc<ISpaceshipsServices>().Update(dto);

            Assert.Equal(spaceship.ID, guid);
            Assert.DoesNotMatch(spaceship.Name, dto.Name);
            Assert.NotEqual(spaceship.PassengerCount, dto.PassengerCount);
        }

        private SpaceshipDto MockSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            { 
                Name = "Testname",
                Description = "Test description",
                PassengerCount = 4,
                CrewCount = 4,
                CargoWeight = 3000,
                MaxSpeedInVaccuum = 200,
                BuiltAtDate = DateTime.Now,
                MaidenLaunch = DateTime.Now,
                Manufacturer = "Test manufacturer",
                IsSpaceshipPreviouslyOwned = true,
                FullTripsCount = 1,
                Type = "Test Type",
                EnginePower = 9001,
                FuelConsumptionPerDay = 4000,
                MaintenanceCount = 0,
                LastMaintenance = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return spaceship;

        }

        [Fact]
        public async Task ShouldNot_UpdateSpaceship_WhenNotUpdateData()
        {
            SpaceshipDto dto = MockSpaceshipData();
            var Spaceship = await Svc<ISpaceshipsServices>().Create(dto);

            SpaceshipDto NullUpdate = MockNotUptadeSpaceship();
            var result = await Svc<ISpaceshipsServices>().Update(NullUpdate);

            var NullID = NullUpdate.ID;

            Assert.True(result.ID != NullID);
        }

        private SpaceshipDto MockNotUptadeSpaceship()
        {
            SpaceshipDto NotSpaceship = new()
            {
                Name = "Testname",
                Description = "Test description",
                PassengerCount = 4,
                CrewCount = 4,
                CargoWeight = 3000,
                MaxSpeedInVaccuum = 200,
                BuiltAtDate = DateTime.Now,
                MaidenLaunch = DateTime.Now,
                Manufacturer = "Test manufacturer",
                IsSpaceshipPreviouslyOwned = true,
                FullTripsCount = 1,
                Type = "Test Type",
                EnginePower = 9001,
                FuelConsumptionPerDay = 4000,
                MaintenanceCount = 0,
                LastMaintenance = DateTime.Now,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };
            return NotSpaceship;
        }
    }
}
