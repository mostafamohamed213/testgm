using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RepositoryPatternWithUOW.EF.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventory_log_operation",
                schema: "inv",
                columns: table => new
                {
                    InventoryLogOperationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OperationName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_log_operation", x => x.InventoryLogOperationID);
                });

            migrationBuilder.CreateTable(
                name: "inventory_log_table",
                schema: "inv",
                columns: table => new
                {
                    InventoryLogTableID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_log_table", x => x.InventoryLogTableID);
                });

            migrationBuilder.CreateTable(
                name: "inventory_log",
                schema: "inv",
                columns: table => new
                {
                    InventoryLogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SystemUserID = table.Column<string>(type: "text", nullable: true),
                    CreateDT = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    InventoryLogTableID = table.Column<int>(type: "integer", nullable: false),
                    InventoryLogOperationID = table.Column<int>(type: "integer", nullable: false),
                    Object1 = table.Column<string>(type: "text", nullable: true),
                    Object2 = table.Column<string>(type: "text", nullable: true),
                    Object3 = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory_log", x => x.InventoryLogID);
                    table.ForeignKey(
                        name: "FK_inventory_log_inventory_log_operation_InventoryLogOperation~",
                        column: x => x.InventoryLogOperationID,
                        principalSchema: "inv",
                        principalTable: "inventory_log_operation",
                        principalColumn: "InventoryLogOperationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_log_inventory_log_table_InventoryLogTableID",
                        column: x => x.InventoryLogTableID,
                        principalSchema: "inv",
                        principalTable: "inventory_log_table",
                        principalColumn: "InventoryLogTableID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_log_InventoryLogOperationID",
                schema: "inv",
                table: "inventory_log",
                column: "InventoryLogOperationID");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_log_InventoryLogTableID",
                schema: "inv",
                table: "inventory_log",
                column: "InventoryLogTableID");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_log_operation_OperationName",
                schema: "inv",
                table: "inventory_log_operation",
                column: "OperationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventory_log_table_TableName",
                schema: "inv",
                table: "inventory_log_table",
                column: "TableName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_log",
                schema: "inv");

            migrationBuilder.DropTable(
                name: "inventory_log_operation",
                schema: "inv");

            migrationBuilder.DropTable(
                name: "inventory_log_table",
                schema: "inv");
        }
    }
}
