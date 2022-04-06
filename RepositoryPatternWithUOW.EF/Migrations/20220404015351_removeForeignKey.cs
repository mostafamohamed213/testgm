using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class removeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_TechnicianI~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropIndex(
                name: "IX_technician_attendance_log_TechnicianId_EventDate",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                schema: "core",
                table: "technician_attendance_log",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "TechnicianAttendanceEventDate",
                schema: "core",
                table: "technician_attendance_log",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicianAttendanceTechnicianId",
                schema: "core",
                table: "technician_attendance_log",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_TechnicianAttendanceTechnicianId_~",
                schema: "core",
                table: "technician_attendance_log",
                columns: new[] { "TechnicianAttendanceTechnicianId", "TechnicianAttendanceEventDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_TechnicianA~",
                schema: "core",
                table: "technician_attendance_log",
                columns: new[] { "TechnicianAttendanceTechnicianId", "TechnicianAttendanceEventDate" },
                principalSchema: "core",
                principalTable: "technician_attendance",
                principalColumns: new[] { "technician_id", "event_date" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_TechnicianA~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropIndex(
                name: "IX_technician_attendance_log_TechnicianAttendanceTechnicianId_~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropColumn(
                name: "TechnicianAttendanceEventDate",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropColumn(
                name: "TechnicianAttendanceTechnicianId",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                schema: "core",
                table: "technician_attendance_log",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_TechnicianId_EventDate",
                schema: "core",
                table: "technician_attendance_log",
                columns: new[] { "TechnicianId", "EventDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_TechnicianI~",
                schema: "core",
                table: "technician_attendance_log",
                columns: new[] { "TechnicianId", "EventDate" },
                principalSchema: "core",
                principalTable: "technician_attendance",
                principalColumns: new[] { "technician_id", "event_date" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
