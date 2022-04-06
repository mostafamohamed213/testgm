using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class deoptablelog1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "technician_attendance_log",
                schema: "core");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "technician_attendance_log",
                schema: "core",
                columns: table => new
                {
                    TechnicianAttendanceLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttendanceStatusId = table.Column<int>(type: "integer", nullable: false),
                    CreateDts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true),
                    Object = table.Column<string>(type: "text", nullable: true),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    Systemusercrate = table.Column<string>(type: "text", nullable: false),
                    TechnicianAttendanceEventDate = table.Column<DateTime>(type: "date", nullable: true),
                    TechnicianAttendanceStatusLogId = table.Column<int>(type: "integer", nullable: false),
                    TechnicianAttendanceTechnicianId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_technician_attendance_log", x => x.TechnicianAttendanceLogId);
                    table.ForeignKey(
                        name: "FK_technician_attendance_log_attendance_status_AttendanceStatu~",
                        column: x => x.AttendanceStatusId,
                        principalSchema: "core",
                        principalTable: "attendance_status",
                        principalColumn: "attendance_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_technician_attendance_log_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "core",
                        principalTable: "shift",
                        principalColumn: "shift_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_technician_attendance_log_technician_attendance_status_log_~",
                        column: x => x.TechnicianAttendanceStatusLogId,
                        principalSchema: "core",
                        principalTable: "technician_attendance_status_log",
                        principalColumn: "TechnicianAttendanceStatusLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_technician_attendance_log_technician_attendance_TechnicianA~",
                        columns: x => new { x.TechnicianAttendanceTechnicianId, x.TechnicianAttendanceEventDate },
                        principalSchema: "core",
                        principalTable: "technician_attendance",
                        principalColumns: new[] { "technician_id", "event_date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_AttendanceStatusId",
                schema: "core",
                table: "technician_attendance_log",
                column: "AttendanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_ShiftId",
                schema: "core",
                table: "technician_attendance_log",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_TechnicianAttendanceStatusLogId",
                schema: "core",
                table: "technician_attendance_log",
                column: "TechnicianAttendanceStatusLogId");

            migrationBuilder.CreateIndex(
                name: "IX_technician_attendance_log_TechnicianAttendanceTechnicianId_~",
                schema: "core",
                table: "technician_attendance_log",
                columns: new[] { "TechnicianAttendanceTechnicianId", "TechnicianAttendanceEventDate" });
        }
    }
}
