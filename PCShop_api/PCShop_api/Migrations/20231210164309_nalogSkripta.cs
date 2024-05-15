using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class nalogSkripta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skracenica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlikaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is2FActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Admin_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Admin_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Kupac",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kupac", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kupac_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kupac_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Radnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZaposlenja = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Radnik_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Radnik_KorisnickiNalog_ID",
                        column: x => x.ID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_DrzavaID",
                table: "Admin",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Kupac_DrzavaID",
                table: "Kupac",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Radnik_DrzavaID",
                table: "Radnik",
                column: "DrzavaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Kupac");

            migrationBuilder.DropTable(
                name: "Radnik");

            migrationBuilder.DropTable(
                name: "Drzava");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");
        }
    }
}
