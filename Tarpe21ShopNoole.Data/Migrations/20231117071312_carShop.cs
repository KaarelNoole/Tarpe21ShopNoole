using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarpe21ShopNoole.Data.Migrations
{
    public partial class carShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "FilesToApi",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GearShiftType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilesToApi_CarId",
                table: "FilesToApi",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilesToApi_Cars_CarId",
                table: "FilesToApi",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilesToApi_Cars_CarId",
                table: "FilesToApi");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_FilesToApi_CarId",
                table: "FilesToApi");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "FilesToApi");
        }
    }
}
