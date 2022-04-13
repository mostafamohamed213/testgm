using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vl2343 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_license",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_license",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_license");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_license");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle");
        }
    }
}
