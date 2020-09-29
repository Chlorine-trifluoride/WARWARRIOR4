using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LeaderboardAPI.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "levels",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_levels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(nullable: true),
                    country_code = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scores",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    time_stamp = table.Column<DateTime>(nullable: false),
                    time_in_seconds = table.Column<int>(nullable: false),
                    high_score = table.Column<int>(nullable: false),
                    playerid = table.Column<int>(nullable: true),
                    levelid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scores", x => x.id);
                    table.ForeignKey(
                        name: "FK_scores_levels_levelid",
                        column: x => x.levelid,
                        principalTable: "levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scores_players_playerid",
                        column: x => x.playerid,
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_scores_levelid",
                table: "scores",
                column: "levelid");

            migrationBuilder.CreateIndex(
                name: "IX_scores_playerid",
                table: "scores",
                column: "playerid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scores");

            migrationBuilder.DropTable(
                name: "levels");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
