using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vfb234 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_family",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_family",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_family",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_brand",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_brand",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_brand",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_family");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_family");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_family");

            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_brand");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_brand");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_brand");
        }
    }
}
