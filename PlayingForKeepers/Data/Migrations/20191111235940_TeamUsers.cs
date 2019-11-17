using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayingForKeepers.Data.Migrations
{
    public partial class TeamUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FF_TeamUsers",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                    //,
                    //UserId = table.Column<string>(nullable: false)
                    //Needs to be added as specific data type for SQL
                });

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "FF_TeamUsers",
                nullable: false,
                type: "nvarchar(450)");


            migrationBuilder.AddForeignKey(
                name: "FK_Teams_TeamUsers_TeamID",
                principalTable: "FF_Teams",
                principalColumn: "TeamID",
                table: "FF_TeamUsers",
                column: "TeamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TeamUsers_UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                table: "FF_TeamUsers",
                column: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TeamUsers_UserID", table: "FF_TeamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_TeamUsers_TeamID", table: "FF_TeamUsers");

            migrationBuilder.DropTable(
                name: "FF_TeamUsers");
        }
    }
}
