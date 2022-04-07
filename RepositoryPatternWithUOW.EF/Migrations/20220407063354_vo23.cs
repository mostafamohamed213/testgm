using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vo23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_owner",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "vehicle_owner",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_owner",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "vehicle_owner");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "vehicle_owner");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "vehicle_owner");
        }
    }
}
