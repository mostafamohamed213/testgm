using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class zxc112 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDts",
                schema: "core",
                table: "tire_size",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemUserCreate",
                schema: "core",
                table: "tire_size",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDts",
                schema: "core",
                table: "tire_size");

            migrationBuilder.DropColumn(
                name: "SystemUserCreate",
                schema: "core",
                table: "tire_size");
        }
    }
}
