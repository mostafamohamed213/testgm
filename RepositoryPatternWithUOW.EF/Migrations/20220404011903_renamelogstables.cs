using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class renamelogstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "technician_attendance_status_log",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "technicianA_attendance_log",
                schema: "core",
                newName: "technician_attendance_log",
                newSchema: "core");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_TechnicianId_EventDate",
                schema: "core",
                table: "technician_attendance_log",
                newName: "IX_technician_attendance_log_TechnicianId_EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_TechnicianAttendanceStatusLogId",
                schema: "core",
                table: "technician_attendance_log",
                newName: "IX_technician_attendance_log_TechnicianAttendanceStatusLogId");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_ShiftId",
                schema: "core",
                table: "technician_attendance_log",
                newName: "IX_technician_attendance_log_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_technicianA_attendance_log_AttendanceStatusId",
                schema: "core",
                table: "technician_attendance_log",
                newName: "IX_technician_attendance_log_AttendanceStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_technician_attendance_status_log",
                schema: "core",
                table: "technician_attendance_status_log",
                column: "TechnicianAttendanceStatusLogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_technician_attendance_log",
                schema: "core",
                table: "technician_attendance_log",
                column: "TechnicianAttendanceLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_technician_attendance_log_attendance_status_AttendanceStatu~",
                schema: "core",
                table: "technician_attendance_log",
                column: "AttendanceStatusId",
                principalSchema: "core",
                principalTable: "attendance_status",
                principalColumn: "attendance_status_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technician_attendance_log_shift_ShiftId",
                schema: "core",
                table: "technician_attendance_log",
                column: "ShiftId",
                principalSchema: "core",
                principalTable: "shift",
                principalColumn: "shift_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_status_log_~",
                schema: "core",
                table: "technician_attendance_log",
                column: "TechnicianAttendanceStatusLogId",
                principalSchema: "core",
                principalTable: "technician_attendance_status_log",
                principalColumn: "TechnicianAttendanceStatusLogId",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_attendance_status_AttendanceStatu~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_shift_ShiftId",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_status_log_~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropForeignKey(
                name: "FK_technician_attendance_log_technician_attendance_TechnicianI~",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_technician_attendance_status_log",
                schema: "core",
                table: "technician_attendance_status_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_technician_attendance_log",
                schema: "core",
                table: "technician_attendance_log");

            migrationBuilder.RenameTable(
                name: "technician_attendance_status_log",
                schema: "core",
                newName: "technicianA_attendance_status_log",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "technician_attendance_log",
                schema: "core",
                newName: "technicianA_attendance_log",
                newSchema: "core");

            migrationBuilder.RenameIndex(
                name: "IX_technician_attendance_log_TechnicianId_EventDate",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_TechnicianId_EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_technician_attendance_log_TechnicianAttendanceStatusLogId",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_TechnicianAttendanceStatusLogId");

            migrationBuilder.RenameIndex(
                name: "IX_technician_attendance_log_ShiftId",
                schema: "core",
                table: "technicianA_attendance_log",
                newName: "IX_technicianA_attendance_log_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_technician_attendance_log_AttendanceStatusId",
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
    }
}
