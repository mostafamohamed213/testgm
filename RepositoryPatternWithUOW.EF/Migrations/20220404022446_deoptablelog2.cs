using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class deoptablelog2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "technician_attendance_log",
                schema: "core",
                columns: table => new
                {
                    TechnicianAttendanceLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Object = table.Column<string>(type: "text", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    CreateDts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Systemusercrate = table.Column<string>(type: "text", nullable: false),
                    AttendanceStatusId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    TechnicianAttendanceStatusLogId = table.Column<int>(type: "integer", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "technician_attendance_log",
                schema: "core");
        }
    }
}
