using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Migrations
{
    public partial class addSellingPriceanditemDetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellingPrice",
                table: "items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "itemDetail",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConsignmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CostPrice = table.Column<int>(type: "int", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemDetail", x => new { x.ConsignmentId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_itemDetail_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itemDetail_ItemId",
                table: "itemDetail",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemDetail");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "items");
        }
    }
}
