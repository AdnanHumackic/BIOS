using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class nalogSkriptaUpd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutentifikacijaToken",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vrijednost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KorisnickiNalogID = table.Column<int>(type: "int", nullable: false),
                    vrijemeEvidentiranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ipAdresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is2FOtkljucano = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutentifikacijaToken", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogID",
                        column: x => x.KorisnickiNalogID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutentifikacijaToken_KorisnickiNalogID",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutentifikacijaToken");
        }
    }
}
