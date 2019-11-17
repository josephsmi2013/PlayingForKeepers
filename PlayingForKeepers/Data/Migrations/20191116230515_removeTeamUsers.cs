using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class removeTeamUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FF_TeamUsers");

            migrationBuilder.AddColumn<string>(
                name: "TeamOwnerID",
                table: "FF_Teams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamOwnerID",
                table: "FF_Teams");

            migrationBuilder.CreateTable(
                name: "FF_TeamUsers",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
