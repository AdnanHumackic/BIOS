using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class updateBazePodataka : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogID",
                table: "AutentifikacijaToken");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogID",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogID",
                table: "AutentifikacijaToken");

            migrationBuilder.AddForeignKey(
                name: "FK_AutentifikacijaToken_KorisnickiNalog_KorisnickiNalogID",
                table: "AutentifikacijaToken",
                column: "KorisnickiNalogID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
