using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class modifyStockCardCustomerWarehouseReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_stockCards_batches_BatchId_WarehouseReceiptId",
                table: "stockCards");

            migrationBuilder.DropIndex(
                name: "IX_stockCards_BatchId_WarehouseReceiptId",
                table: "stockCards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropColumn(
                name: "WarehouseReceiptId",
                table: "stockCards");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseReceiptId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_stockCards_BatchId",
                table: "stockCards",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                unique: true,
                filter: "[WarehouseReceiptId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches",
                column: "WarehouseReceiptId",
                principalTable: "warehouseReceipts",
                principalColumn: "WarehouseReceiptId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_CustomerId",
                table: "orders",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_stockCards_batches_BatchId",
                table: "stockCards",
                column: "BatchId",
                principalTable: "batches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_CustomerId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_stockCards_batches_BatchId",
                table: "stockCards");

            migrationBuilder.DropIndex(
                name: "IX_stockCards_BatchId",
                table: "stockCards");

            migrationBuilder.DropIndex(
                name: "IX_orders_CustomerId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseReceiptId",
                table: "stockCards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_stockCards_BatchId_WarehouseReceiptId",
                table: "stockCards",
                columns: new[] { "BatchId", "WarehouseReceiptId" });

            migrationBuilder.CreateIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                table: "batches",
                column: "WarehouseReceiptId",
                principalTable: "warehouseReceipts",
                principalColumn: "WarehouseReceiptId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stockCards_batches_BatchId_WarehouseReceiptId",
                table: "stockCards",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                principalTable: "batches",
                principalColumns: new[] { "BatchId", "WarehouseReceiptId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
