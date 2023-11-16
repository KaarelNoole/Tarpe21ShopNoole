using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarpe21ShopNoole.Data.Migrations
{
    public partial class NoDimension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dimension");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    DimensionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    SpaceShipID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: false)
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
    }
}
