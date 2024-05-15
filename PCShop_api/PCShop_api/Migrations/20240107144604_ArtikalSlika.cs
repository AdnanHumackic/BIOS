using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class ArtikalSlika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtikalSlika",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalID = table.Column<int>(type: "int", nullable: false),
                    SlikaArtikla = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtikalSlika", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtikalSlika_Artikal_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtikalSlika_ArtikalID",
                table: "ArtikalSlika",
                column: "ArtikalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtikalSlika");
        }
    }
}
