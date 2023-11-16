﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarpe21ShopNoole.Data.Migrations
{
    public partial class dtocore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spaceShips",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengerCount = table.Column<int>(type: "int", nullable: false),
                    CrewCount = table.Column<int>(type: "int", nullable: false),
                    CargoWeight = table.Column<int>(type: "int", nullable: false),
                    MaxSpeedInVaccuum = table.Column<int>(type: "int", nullable: false),
                    BuiltAtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaidenLaunch = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSpaceshipPreviouslyOwned = table.Column<bool>(type: "bit", nullable: false),
                    FullTripsCount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnginePower = table.Column<int>(type: "int", nullable: false),
                    FuelConsumptionPerDay = table.Column<int>(type: "int", nullable: false),
                    MaintenanceCount = table.Column<int>(type: "int", nullable: false),
                    LastMaintenance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spaceShips", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    DimensionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    SpaceShipID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.DimensionID);
                    table.ForeignKey(
                        name: "FK_Dimension_spaceShips_SpaceShipID",
                        column: x => x.SpaceShipID,
                        principalTable: "spaceShips",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dimension_SpaceShipID",
                table: "Dimension",
                column: "SpaceShipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "spaceShips");
        }
    }
}
