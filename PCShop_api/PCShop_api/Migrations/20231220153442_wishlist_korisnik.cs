using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class wishlist_korisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidentiraoKorisnikId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Narudzba",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dostavljac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UkupnaCijena = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narudzba", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlist_EvidentiraoKorisnikId",
                table: "Wishlist",
                column: "EvidentiraoKorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlist_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Wishlist",
                column: "EvidentiraoKorisnikId",
                principalTable: "KorisnickiNalog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlist_KorisnickiNalog_EvidentiraoKorisnikId",
                table: "Wishlist");

            migrationBuilder.DropTable(
                name: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Wishlist_EvidentiraoKorisnikId",
                table: "Wishlist");

            migrationBuilder.DropColumn(
                name: "EvidentiraoKorisnikId",
                table: "Wishlist");
        }
    }
}
