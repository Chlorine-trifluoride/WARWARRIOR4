using Microsoft.EntityFrameworkCore.Migrations;

namespace LeaderboardAPI.Migrations
{
    public partial class LevelData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "levels",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Level 1" },
                    { 2, "Level 2" },
                    { 3, "Level 3" },
                    { 4, "Level 4" },
                    { 5, "Level 5" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "levels",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "levels",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "levels",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "levels",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "levels",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
