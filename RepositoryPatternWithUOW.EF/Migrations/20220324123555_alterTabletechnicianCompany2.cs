﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class alterTabletechnicianCompany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "technician_company",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Enable",
                schema: "core",
                table: "technician_company",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
