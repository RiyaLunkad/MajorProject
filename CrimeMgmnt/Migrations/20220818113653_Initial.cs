using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrimeMgmnt.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    CrimePlatform = table.Column<string>(nullable: false),
                    CrimeDescription = table.Column<string>(maxLength: 50, nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ControlRoom",
                columns: table => new
                {
                    ControlRoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlRoom", x => x.ControlRoomId);
                    table.ForeignKey(
                        name: "FK_ControlRoom_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControlRoom_UserId",
                table: "ControlRoom",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlRoom");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
