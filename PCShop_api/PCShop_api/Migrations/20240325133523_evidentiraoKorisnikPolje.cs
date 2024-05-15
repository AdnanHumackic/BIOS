using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class evidentiraoKorisnikPolje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidentiraoKorisnikID",
                table: "Dokumenti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_EvidentiraoKorisnikID",
                table: "Dokumenti",
                column: "EvidentiraoKorisnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumenti_KorisnickiNalog_EvidentiraoKorisnikID",
                table: "Dokumenti",
                column: "EvidentiraoKorisnikID",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumenti_KorisnickiNalog_EvidentiraoKorisnikID",
                table: "Dokumenti");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_EvidentiraoKorisnikID",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "EvidentiraoKorisnikID",
                table: "Dokumenti");
        }
    }
}
