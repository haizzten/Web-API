using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Migrations
{
    public partial class modifyStaffsprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetail_items_ItemId",
                table: "ItemDetail");

            migrationBuilder.AlterColumn<string>(
                name: "StaffName",
                table: "staffs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "staffs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDetail_items_ItemId",
                table: "ItemDetail",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetail_items_ItemId",
                table: "ItemDetail");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "staffs");

            migrationBuilder.AlterColumn<string>(
                name: "StaffName",
                table: "staffs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDetail_items_ItemId",
                table: "ItemDetail",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "ItemId");
        }
    }
}
