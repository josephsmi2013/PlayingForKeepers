using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class LeagueUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddPrimaryKey(
                name: "PK_Leagues_LeagueID",
                table: "FF_Leagues",
                column: "LeagueID");

            migrationBuilder.CreateTable(
                name: "FF_LeagueUsers",
                columns: table => new
                {
                    LeagueID = table.Column<int>(nullable: false)
                    //,
                    //UserID = table.Column<string>(nullable: false)
                    //Needs to be added as specific data type for SQL
                });

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "FF_LeagueUsers",
                nullable: false,
                type: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
            name: "FK_Leagues_LeagueUsers_LeagueID",
            principalTable: "FF_Leagues",
            principalColumn: "LeagueID",
            table: "FF_LeagueUsers",
            column: "LeagueID",
            onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
            name: "FK_Users_LeagueUsers_UserID",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            table: "FF_LeagueUsers",
            column: "UserID",
            onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_LeagueUsers_UserID", table: "FF_LeagueUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_LeagueUsers_LeagueID", table: "FF_LeagueUsers");

            migrationBuilder.DropTable(
                name: "FF_LeagueUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leagues_LeagueID",
                table: "FF_Leagues");
        }
    }
}
