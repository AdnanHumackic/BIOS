using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class recenzi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recenzije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sadrzaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EvidentiraoKorisnikId = table.Column<int>(type: "int", nullable: false),
                    ArtikalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recenzije_Artikal_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recenzije_KorisnickiNalog_EvidentiraoKorisnikId",
                        column: x => x.EvidentiraoKorisnikId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_ArtikalId",
                table: "Recenzije",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzije_EvidentiraoKorisnikId",
                table: "Recenzije",
                column: "EvidentiraoKorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recenzije");
        }
    }
}
