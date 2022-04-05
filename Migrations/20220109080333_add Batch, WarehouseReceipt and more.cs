using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class addBatchWarehouseReceiptandmore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDetail");

            migrationBuilder.AddColumn<int>(
                name: "InSelling",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InStock",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotifyBeforeDays",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "warehouseReceipts",
                columns: table => new
                {
                    WarehouseReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelivererName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachedOriginalDocumentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouseReceipts", x => x.WarehouseReceiptId);
                });

            migrationBuilder.CreateTable(
                name: "batches",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Remain = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarehouseReceiptId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_batches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_batches_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_batches_providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_batches_warehouseReceipts_WarehouseReceiptId",
                        column: x => x.WarehouseReceiptId,
                        principalTable: "warehouseReceipts",
                        principalColumn: "WarehouseReceiptId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batches_BatchId_WarehouseReceiptId",
                table: "batches",
                columns: new[] { "BatchId", "WarehouseReceiptId" },
                unique: true,
                filter: "[WarehouseReceiptId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_batches_ItemId",
                table: "batches",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_batches_ProviderId",
                table: "batches",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_batches_WarehouseReceiptId",
                table: "batches",
                column: "WarehouseReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "batches");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "warehouseReceipts");

            migrationBuilder.DropColumn(
                name: "InSelling",
                table: "items");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "items");

            migrationBuilder.DropColumn(
                name: "NotifyBeforeDays",
                table: "items");

            migrationBuilder.CreateTable(
                name: "ItemDetail",
                columns: table => new
                {
                    ConsignmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CostPrice = table.Column<int>(type: "int", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetail", x => new { x.ConsignmentId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ItemDetail_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetail_ItemId",
                table: "ItemDetail",
                column: "ItemId");
        }
    }
}
