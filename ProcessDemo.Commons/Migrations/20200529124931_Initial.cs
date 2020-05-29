using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessDemo.Commons.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    FarmId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.FarmId);
                });

            migrationBuilder.CreateTable(
                name: "AppleTrees",
                columns: table => new
                {
                    AppleTreeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppleYield = table.Column<double>(nullable: false),
                    WaterConsumption = table.Column<double>(nullable: false),
                    FertilizingAgent = table.Column<int>(nullable: false),
                    FarmId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleTrees", x => x.AppleTreeId);
                    table.ForeignKey(
                        name: "FK_AppleTrees_Farms_FarmId",
                        column: x => x.FarmId,
                        principalTable: "Farms",
                        principalColumn: "FarmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppleTrees_FarmId",
                table: "AppleTrees",
                column: "FarmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppleTrees");

            migrationBuilder.DropTable(
                name: "Farms");
        }
    }
}
