using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class addStaffIdtoPurchaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "purchaseOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchaseOrders_StaffId",
                table: "purchaseOrders",
                column: "StaffId",
                unique: true,
                filter: "[StaffId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseOrders_staffs_StaffId",
                table: "purchaseOrders",
                column: "StaffId",
                principalTable: "staffs",
                principalColumn: "StaffId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseOrders_staffs_StaffId",
                table: "purchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_purchaseOrders_StaffId",
                table: "purchaseOrders");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "purchaseOrders");
        }
    }
}
