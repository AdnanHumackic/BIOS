using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class zadaci_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zadatak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RadnikID = table.Column<int>(type: "int", nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadatak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zadatak_Radnik_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_RadnikID",
                table: "Zadatak",
                column: "RadnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zadatak");
        }
    }
}
