using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vd34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_department",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_department",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_department",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_department");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_department");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_department");
        }
    }
}
