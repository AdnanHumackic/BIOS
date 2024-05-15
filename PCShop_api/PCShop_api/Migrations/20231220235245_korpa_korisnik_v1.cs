using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class korpa_korisnik_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidentiraoKorisnikId",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_EvidentiraoKorisnikId",
                table: "Korpa",
                column: "EvidentiraoKorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Korpa",
                column: "EvidentiraoKorisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_EvidentiraoKorisnikId",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "EvidentiraoKorisnikId",
                table: "Korpa");
        }
    }
}
