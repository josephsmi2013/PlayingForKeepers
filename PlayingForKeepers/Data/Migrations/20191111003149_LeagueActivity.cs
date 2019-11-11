using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class LeagueActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "FF_LeagueActivity",
                columns: table => new
                {
                    LeagueActivityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueID = table.Column<int>(nullable: false),
                    LeagueActivityDate = table.Column<DateTime>(nullable: false),
                    LeagueActivityDescription = table.Column<string>(nullable: true),
                    LeagueActivityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueActivity_LeagueActivityID", x => x.LeagueActivityID);
                });

                migrationBuilder.AddForeignKey(
                    name: "FK_Leagues_LeagueActivity_LeagueID",
                    principalTable: "FF_Leagues",
                    principalColumn: "LeagueID",
                    table: "FF_LeagueActivity",
                    column: "LeagueID",
                    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueActivity_LeagueID", 
                table: "FF_LeagueActivity");

            migrationBuilder.DropTable(
                name: "FF_LeagueActivity");

            
        }
    }
}
