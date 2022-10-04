using Microsoft.EntityFrameworkCore.Migrations;

namespace CrimeMgmnt.Migrations
{
    public partial class AddedNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mobile",
                table: "Users",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mobile",
                table: "Users");
        }
    }
}
