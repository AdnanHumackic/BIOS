using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class zadaci_finale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidentiraoKorisnikId",
                table: "Zadatak",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_EvidentiraoKorisnikId",
                table: "Zadatak",
                column: "EvidentiraoKorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadatak_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Zadatak",
                column: "EvidentiraoKorisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadatak_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Zadatak");

            migrationBuilder.DropIndex(
                name: "IX_Zadatak_EvidentiraoKorisnikId",
                table: "Zadatak");

            migrationBuilder.DropColumn(
                name: "EvidentiraoKorisnikId",
                table: "Zadatak");
        }
    }
}
