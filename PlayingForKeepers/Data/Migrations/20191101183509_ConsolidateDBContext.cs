using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class ConsolidateDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "FF_AddLeague",
            //    columns: table => new
            //    {
            //        LeagueName = table.Column<string>(nullable: true),
            //        LeagueTeamsPossible = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FF_GetLeagues",
            //    columns: table => new
            //    {
            //        LeagueID = table.Column<int>(nullable: false),
            //        LeagueName = table.Column<string>(nullable: true),
            //        LeagueTeamsUsed = table.Column<int>(nullable: false),
            //        LeagueTeamsPossible = table.Column<int>(nullable: false),
            //        LeagueOwnerID = table.Column<string>(nullable: true),
            //        LeagueStatus = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FF_Leagues",
            //    columns: table => new
            //    {
            //        LeagueID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LeagueName = table.Column<string>(maxLength: 64, nullable: false),
            //        LeagueTeamsUsed = table.Column<int>(nullable: false),
            //        LeagueTeamsPossible = table.Column<int>(nullable: false),
            //        LeagueOwnerID = table.Column<string>(nullable: true),
            //        LeagueStatus = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "FF_AddLeague");

            //migrationBuilder.DropTable(
            //    name: "FF_GetLeagues");

            //migrationBuilder.DropTable(
            //    name: "FF_Leagues");
        }
    }
}
