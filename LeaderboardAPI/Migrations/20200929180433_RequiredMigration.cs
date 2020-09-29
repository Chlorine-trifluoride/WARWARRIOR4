using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderboardAPI.Migrations
{
    public partial class RequiredMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_scores_levels_levelid",
                table: "scores");

            migrationBuilder.DropForeignKey(
                name: "FK_scores_players_playerid",
                table: "scores");

            migrationBuilder.AlterColumn<int>(
                name: "playerid",
                table: "scores",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "levelid",
                table: "scores",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "players",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_scores_levels_levelid",
                table: "scores",
                column: "levelid",
                principalTable: "levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_scores_players_playerid",
                table: "scores",
                column: "playerid",
                principalTable: "players",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_scores_levels_levelid",
                table: "scores");

            migrationBuilder.DropForeignKey(
                name: "FK_scores_players_playerid",
                table: "scores");

            migrationBuilder.AlterColumn<int>(
                name: "playerid",
                table: "scores",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "levelid",
                table: "scores",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "players",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_scores_levels_levelid",
                table: "scores",
                column: "levelid",
                principalTable: "levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_scores_players_playerid",
                table: "scores",
                column: "playerid",
                principalTable: "players",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
