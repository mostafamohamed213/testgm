using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class attendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Systemusercrate",
                schema: "core",
                table: "technician_attendance",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TechnicianAttendanceStatusLogs",
                columns: table => new
                {
                    TechnicianAttendanceStatusLogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAttendanceStatusLogs", x => x.TechnicianAttendanceStatusLogId);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianAttendanceLogs",
                columns: table => new
                {
                    TechnicianAttendanceLogId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Object = table.Column<string>(type: "text", nullable: true),
                    Details = table.Column<string>(type: "text", nullable: true),
                    CreateDts = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Systemusercrate = table.Column<string>(type: "text", nullable: false),
                    TechnicianId = table.Column<int>(type: "integer", nullable: false),
                    EventDate = table.Column<DateTime>(type: "date", nullable: false),
                    AttendanceStatusId = table.Column<int>(type: "integer", nullable: false),
                    ShiftId = table.Column<int>(type: "integer", nullable: false),
                    TechnicianAttendanceStatusLogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianAttendanceLogs", x => x.TechnicianAttendanceLogId);
                    table.ForeignKey(
                        name: "FK_TechnicianAttendanceLogs_attendance_status_AttendanceStatus~",
                        column: x => x.AttendanceStatusId,
                        principalSchema: "core",
                        principalTable: "attendance_status",
                        principalColumn: "attendance_status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianAttendanceLogs_shift_ShiftId",
                        column: x => x.ShiftId,
                        principalSchema: "core",
                        principalTable: "shift",
                        principalColumn: "shift_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianAttendanceLogs_technician_attendance_TechnicianId~",
                        columns: x => new { x.TechnicianId, x.EventDate },
                        principalSchema: "core",
                        principalTable: "technician_attendance",
                        principalColumns: new[] { "technician_id", "event_date" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogs_Tec~",
                        column: x => x.TechnicianAttendanceStatusLogId,
                        principalTable: "TechnicianAttendanceStatusLogs",
                        principalColumn: "TechnicianAttendanceStatusLogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttendanceLogs_AttendanceStatusId",
                table: "TechnicianAttendanceLogs",
                column: "AttendanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttendanceLogs_ShiftId",
                table: "TechnicianAttendanceLogs",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttendanceLogs_TechnicianAttendanceStatusLogId",
                table: "TechnicianAttendanceLogs",
                column: "TechnicianAttendanceStatusLogId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianAttendanceLogs_TechnicianId_EventDate",
                table: "TechnicianAttendanceLogs",
                columns: new[] { "TechnicianId", "EventDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicianAttendanceLogs");

            migrationBuilder.DropTable(
                name: "TechnicianAttendanceStatusLogs");

            migrationBuilder.DropColumn(
                name: "Systemusercrate",
                schema: "core",
                table: "technician_attendance");
        }
    }
}
