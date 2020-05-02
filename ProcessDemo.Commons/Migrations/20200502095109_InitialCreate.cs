using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessDemo.Commons.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Create the AppleTrees table
            migrationBuilder.CreateTable(
                name: "AppleTrees",
                //Define the columns
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreeNumber = table.Column<int>(nullable: false),
                    AppleYield = table.Column<double>(nullable: false),
                    WaterConsumption = table.Column<double>(nullable: false),
                    FertilizingAgent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    //Id is the identity column since we marked it with the "Key" attribute
                    table.PrimaryKey("PK_AppleTrees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppleTrees");
        }
    }
}
