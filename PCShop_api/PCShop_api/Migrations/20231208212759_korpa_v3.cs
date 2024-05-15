using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class korpa_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdresaDostave",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "NacinPlacanja",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "Napomena",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "UkupnaCijena",
                table: "Korpa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdresaDostave",
                table: "Korpa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NacinPlacanja",
                table: "Korpa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Napomena",
                table: "Korpa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "UkupnaCijena",
                table: "Korpa",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
