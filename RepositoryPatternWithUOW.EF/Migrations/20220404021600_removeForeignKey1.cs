using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class removeForeignKey1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                schema: "core",
                table: "technician_attendance_log");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                schema: "core",
                table: "technician_attendance_log",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TechnicianId",
                schema: "core",
                table: "technician_attendance_log",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
