using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vt12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_status",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_status",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_status",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_status");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_status");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_status");
        }
    }
}
