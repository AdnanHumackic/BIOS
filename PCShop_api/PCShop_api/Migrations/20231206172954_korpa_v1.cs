using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class korpa_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumDodavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdresaDostave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UkupnaCijena = table.Column<float>(type: "real", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtikalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korpa_Artikal_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_ArtikalID",
                table: "Korpa",
                column: "ArtikalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korpa");
        }
    }
}
