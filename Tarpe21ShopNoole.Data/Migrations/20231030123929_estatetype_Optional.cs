﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarpe21ShopNoole.Data.Migrations
{
    public partial class estatetype_Optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstateType",
                table: "RealEstates");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RealEstates",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RealEstates");

            migrationBuilder.AddColumn<int>(
                name: "EstateType",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
