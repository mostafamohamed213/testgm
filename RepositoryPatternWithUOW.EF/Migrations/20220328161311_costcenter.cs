using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class costcenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "cost_center",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "cost_center",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Systemusercrate",
                schema: "core",
                table: "cost_center",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "cost_center");

            migrationBuilder.DropColumn(
                name: "Enable",
                schema: "core",
                table: "cost_center");

            migrationBuilder.DropColumn(
                name: "Systemusercrate",
                schema: "core",
                table: "cost_center");
        }
    }
}
