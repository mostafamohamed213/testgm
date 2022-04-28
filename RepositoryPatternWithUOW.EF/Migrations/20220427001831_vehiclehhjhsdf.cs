using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class vehiclehhjhsdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                schema: "inv",
                table: "inventory_item_assignment",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                schema: "inv",
                table: "inventory_item_assignment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateTime",
                schema: "inv",
                table: "inventory_item_assignment");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                schema: "inv",
                table: "inventory_item_assignment");
        }
    }
}
