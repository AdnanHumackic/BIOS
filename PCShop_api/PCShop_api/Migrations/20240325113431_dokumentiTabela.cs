using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class dokumentiTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokumenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivFajla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifraFajla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidentiraoKorisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dokumenti_KorisnickiNalog_EvidentiraoKorisnikID",
                        column: x => x.EvidentiraoKorisnikID,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_EvidentiraoKorisnikID",
                table: "Dokumenti",
                column: "EvidentiraoKorisnikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dokumenti");
        }
    }
}
