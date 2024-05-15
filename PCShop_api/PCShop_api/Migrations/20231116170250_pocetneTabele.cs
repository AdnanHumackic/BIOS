using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class pocetneTabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipArtikla",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipArtikla", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeArtikla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proizvodjac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cijena = table.Column<int>(type: "int", nullable: false),
                    TipID = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Artikal_TipArtikla_TipID",
                        column: x => x.TipID,
                        principalTable: "TipArtikla",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kompatibilnost",
                columns: table => new
                {
                    ArtikalKompatibilnostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artikal1ID = table.Column<int>(type: "int", nullable: false),
                    Artikal2ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompatibilnost", x => x.ArtikalKompatibilnostID);
                    table.ForeignKey(
                        name: "FK_Kompatibilnost_Artikal_Artikal1ID",
                        column: x => x.Artikal1ID,
                        principalTable: "Artikal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kompatibilnost_Artikal_Artikal2ID",
                        column: x => x.Artikal2ID,
                        principalTable: "Artikal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_TipID",
                table: "Artikal",
                column: "TipID");

            migrationBuilder.CreateIndex(
                name: "IX_Kompatibilnost_Artikal1ID",
                table: "Kompatibilnost",
                column: "Artikal1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Kompatibilnost_Artikal2ID",
                table: "Kompatibilnost",
                column: "Artikal2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kompatibilnost");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "TipArtikla");
        }
    }
}
