using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class mailRadnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Radnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Radnik");
        }
    }
}
