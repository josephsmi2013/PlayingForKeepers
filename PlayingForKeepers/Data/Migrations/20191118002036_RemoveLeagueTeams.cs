using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class RemoveLeagueTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FF_LeagueTeams");

            migrationBuilder.AddColumn<int>(
                name: "LeagueID",
                table: "FF_Teams",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Teams_LeagueID",
                principalTable: "FF_Leagues",
                principalColumn: "LeagueID",
                table: "FF_Teams",
                column: "LeagueID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Teams_LeagueID", table: "FF_Teams");

            migrationBuilder.DropColumn(
                name: "LeagueID",
                table: "FF_Teams");

            migrationBuilder.CreateTable(
                name: "FF_LeagueTeams",
                columns: table => new
                {
                    LeagueID = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
