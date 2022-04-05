using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class addStockCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_items_ItemId",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseReceiptId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" });

            migrationBuilder.CreateTable(
                name: "stockCards",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    In = table.Column<int>(type: "int", nullable: true),
                    Out = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<int>(type: "int", nullable: true),
                    BatchId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WarehouseReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockCards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_stockCards_batches_BatchId_WarehouseReceiptId",
                        columns: x => new { x.BatchId, x.WarehouseReceiptId },
                        principalTable: "batches",
                        principalColumns: new[] { "BatchId", "WarehouseReceiptId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stockCards_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stockCards_BatchId_WarehouseReceiptId",
                table: "stockCards",
                columns: new[] { "BatchId", "WarehouseReceiptId" });

            migrationBuilder.CreateIndex(
                name: "IX_stockCards_ItemId_DateTime",
                table: "stockCards",
                columns: new[] { "ItemId", "DateTime" },
                unique: true,
                filter: "[ItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_items_ItemId",
                table: "batches",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches",
                column: "WarehouseReceiptId",
                principalTable: "warehouseReceipts",
                principalColumn: "WarehouseReceiptId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_items_ItemId",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropTable(
                name: "stockCards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseReceiptId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                unique: true,
                filter: "[WarehouseReceiptId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_items_ItemId",
                table: "batches",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches",
                column: "WarehouseReceiptId",
                principalTable: "warehouseReceipts",
                principalColumn: "WarehouseReceiptId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
