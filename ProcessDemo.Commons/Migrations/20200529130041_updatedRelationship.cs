using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessDemo.Commons.Migrations
{
    public partial class updatedRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppleTrees",
                table: "AppleTrees");

            migrationBuilder.DropColumn(
                name: "AppleTreeId",
                table: "AppleTrees");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppleTrees",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppleTrees",
                table: "AppleTrees",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppleTrees",
                table: "AppleTrees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppleTrees");

            migrationBuilder.AddColumn<int>(
                name: "AppleTreeId",
                table: "AppleTrees",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppleTrees",
                table: "AppleTrees",
                column: "AppleTreeId");
        }
    }
}
