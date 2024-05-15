using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace PCShop_api.Migrations
{
    public partial class korpa_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NacinPlacanja",
                table: "Korpa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NacinPlacanja",
                table: "Korpa");
        }
    }
}
