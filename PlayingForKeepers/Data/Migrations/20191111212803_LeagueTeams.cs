using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class LeagueTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "FF_Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(nullable: false),
                    TeamAbbr = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams_TeamID", x => x.TeamID);
                });


            migrationBuilder.CreateTable(
                name: "FF_LeagueTeams",
                columns: table => new
                {
                    LeagueID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_LeagueTeams_LeagueID",
                principalTable: "FF_Leagues",
                principalColumn: "LeagueID",
                table: "FF_LeagueTeams",
                column: "LeagueID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_LeagueTeams_TeamID",
                principalTable: "FF_Teams",
                principalColumn: "TeamID",
                table: "FF_LeagueTeams",
                column: "TeamID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_LeagueTeams_TeamID",
                table: "FF_LeagueTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueTeams_LeagueID",
                table: "FF_LeagueTeams");

            migrationBuilder.DropTable(
                name: "FF_LeagueTeams");


            migrationBuilder.DropTable(
                name: "FF_Teams");

        }
    }
}
