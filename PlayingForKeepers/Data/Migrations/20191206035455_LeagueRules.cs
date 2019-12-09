using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class LeagueRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FF_LeagueRules",
                columns: table => new
                {
                    LeagueRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueID = table.Column<int>(nullable: false),
                    LeagueRuleDescription = table.Column<string>(nullable: true),
                    LeagueRuleCreatedDate = table.Column<DateTime>(nullable: false),
                    LeagueRuleModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueRules_LeagueRuleID", x => x.LeagueRuleID);
                });

            migrationBuilder.AddForeignKey(
            name: "FK_Leagues_LeagueRules_LeagueID",
            principalTable: "FF_Leagues",
            principalColumn: "LeagueID",
            table: "FF_LeagueRules",
            column: "LeagueID",
            onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueRules_LeagueID", table: "FF_LeagueRules");

            migrationBuilder.DropTable(
                name: "FF_LeagueRules");
        }
    }
}
