using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class addPurchaseOrderDetailandPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachedOriginalDocumentId",
                table: "warehouseReceipts");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseOrderId",
                table: "warehouseReceipts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "purchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_purchaseOrders_providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "purchaseOrdersDetail",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PurchaseOrderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchaseOrdersDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_purchaseOrdersDetail_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchaseOrdersDetail_purchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "purchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_warehouseReceipts_PurchaseOrderId",
                table: "warehouseReceipts",
                column: "PurchaseOrderId",
                unique: true,
                filter: "[PurchaseOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_ProviderId",
                table: "purchaseOrders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrdersDetail_ItemId",
                table: "purchaseOrdersDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrdersDetail_PurchaseOrderId",
                table: "purchaseOrdersDetail",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_warehouseReceipts_purchaseOrders_PurchaseOrderId",
                table: "warehouseReceipts",
                column: "PurchaseOrderId",
                principalTable: "purchaseOrders",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_warehouseReceipts_purchaseOrders_PurchaseOrderId",
                table: "warehouseReceipts");

            migrationBuilder.DropTable(
                name: "purchaseOrdersDetail");

            migrationBuilder.DropTable(
                name: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_warehouseReceipts_PurchaseOrderId",
                table: "warehouseReceipts");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "warehouseReceipts");

            migrationBuilder.AddColumn<string>(
                name: "AttachedOriginalDocumentId",
                table: "warehouseReceipts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
