using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class attendance2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianAttendanceLogs_attendance_status_AttendanceStatus~",
                table: "TechnicianAttendanceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianAttendanceLogs_shift_ShiftId",
                table: "TechnicianAttendanceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianAttendanceLogs_technician_attendance_TechnicianId~",
                table: "TechnicianAttendanceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogs_Tec~",
                table: "TechnicianAttendanceLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianAttendanceStatusLogs",
                table: "TechnicianAttendanceStatusLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianAttendanceLogs",
                table: "TechnicianAttendanceLogs");

            migrationBuilder.RenameTable(
                name: "TechnicianAttendanceStatusLogs",
                newName: "technicianA_attendance_status_log",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "TechnicianAttendanceLogs",
                newName: "technicianA_attendance_log",
                newSchema: "core");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianAttendanceLogs_TechnicianId_EventDate",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_TechnicianId_EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogId",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_TechnicianAttendanceStatusLogId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianAttendanceLogs_ShiftId",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianAttendanceLogs_AttendanceStatusId",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_AttendanceStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_technicianA_attendance_status_log",
                schema: "core",
                table: "technicianA_attendance_status_log",
                column: "TechnicianAttendanceStatusLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_technicianA_attendance_log",
                schema: "core",
                table: "technicianA_attendance_log",
                column: "TechnicianAttendanceLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_technicianA_attendance_log_attendance_status_AttendanceStat~",
                schema: "core",
                table: "technicianA_attendance_log",
                column: "AttendanceStatusId",
                principalSchema: "core",
                principalTable: "attendance_status",
                principalColumn: "attendance_status_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technicianA_attendance_log_shift_ShiftId",
                schema: "core",
                table: "technicianA_attendance_log",
                column: "ShiftId",
                principalSchema: "core",
                principalTable: "shift",
                principalColumn: "shift_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technicianA_attendance_log_technician_attendance_Technician~",
                schema: "core",
                table: "technicianA_attendance_log",
                columns: new[] { "TechnicianId", "EventDate" },
                principalSchema: "core",
                principalTable: "technician_attendance",
                principalColumns: new[] { "technician_id", "event_date" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technicianA_attendance_log_technicianA_attendance_status_lo~",
                schema: "core",
                table: "technicianA_attendance_log",
                column: "TechnicianAttendanceStatusLogId",
                principalSchema: "core",
                principalTable: "technicianA_attendance_status_log",
                principalColumn: "TechnicianAttendanceStatusLogId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_technicianA_attendance_log_attendance_status_AttendanceStat~",
                schema: "core",
                table: "technicianA_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technicianA_attendance_log_shift_ShiftId",
                schema: "core",
                table: "technicianA_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technicianA_attendance_log_technician_attendance_Technician~",
                schema: "core",
                table: "technicianA_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technicianA_attendance_log_technicianA_attendance_status_lo~",
                schema: "core",
                table: "technicianA_attendance_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_technicianA_attendance_status_log",
                schema: "core",
                table: "technicianA_attendance_status_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_technicianA_attendance_log",
                schema: "core",
                table: "technicianA_attendance_log");

            migrationBuilder.RenameTable(
                name: "technicianA_attendance_status_log",
                schema: "core",
                newName: "TechnicianAttendanceStatusLogs");

            migrationBuilder.RenameTable(
                name: "technicianA_attendance_log",
                schema: "core",
                newName: "TechnicianAttendanceLogs");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_TechnicianId_EventDate",
                table: "TechnicianAttendanceLogs",
                newName: "IX_TechnicianAttendanceLogs_TechnicianId_EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_TechnicianAttendanceStatusLogId",
                table: "TechnicianAttendanceLogs",
                newName: "IX_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogId");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_ShiftId",
                table: "TechnicianAttendanceLogs",
                newName: "IX_TechnicianAttendanceLogs_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_AttendanceStatusId",
                table: "TechnicianAttendanceLogs",
                newName: "IX_TechnicianAttendanceLogs_AttendanceStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianAttendanceStatusLogs",
                table: "TechnicianAttendanceStatusLogs",
                column: "TechnicianAttendanceStatusLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianAttendanceLogs",
                table: "TechnicianAttendanceLogs",
                column: "TechnicianAttendanceLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianAttendanceLogs_attendance_status_AttendanceStatus~",
                table: "TechnicianAttendanceLogs",
                column: "AttendanceStatusId",
                principalSchema: "core",
                principalTable: "attendance_status",
                principalColumn: "attendance_status_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianAttendanceLogs_shift_ShiftId",
                table: "TechnicianAttendanceLogs",
                column: "ShiftId",
                principalSchema: "core",
                principalTable: "shift",
                principalColumn: "shift_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianAttendanceLogs_technician_attendance_TechnicianId~",
                table: "TechnicianAttendanceLogs",
                columns: new[] { "TechnicianId", "EventDate" },
                principalSchema: "core",
                principalTable: "technician_attendance",
                principalColumns: new[] { "technician_id", "event_date" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogs_Tec~",
                table: "TechnicianAttendanceLogs",
                column: "TechnicianAttendanceStatusLogId",
                principalTable: "TechnicianAttendanceStatusLogs",
                principalColumn: "TechnicianAttendanceStatusLogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
