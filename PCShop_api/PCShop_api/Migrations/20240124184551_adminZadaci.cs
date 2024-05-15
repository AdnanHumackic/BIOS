using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCShop_api.Migrations
{
    public partial class adminZadaci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Zadatak",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Zadatak_AdminID",
                table: "Zadatak",
                column: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zadatak_Admin_AdminID",
                table: "Zadatak",
                column: "AdminID",
                principalTable: "Admin",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zadatak_Admin_AdminID",
                table: "Zadatak");

            migrationBuilder.DropIndex(
                name: "IX_Zadatak_AdminID",
                table: "Zadatak");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Zadatak");
        }
    }
}
