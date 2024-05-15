using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class Narudzba_finale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidentiraoKorisnikId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_EvidentiraoKorisnikId",
                table: "Narudzba",
                column: "EvidentiraoKorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Narudzba",
                column: "EvidentiraoKorisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_EvidentiraoKorisnikId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "EvidentiraoKorisnikId",
                table: "Narudzba");
        }
    }
}
