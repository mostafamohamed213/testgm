using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vehiclehhjhsdfhj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                schema: "inv",
                table: "inventory_item_assignment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                schema: "inv",
                table: "inventory_item_assignment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
