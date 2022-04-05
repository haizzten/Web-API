using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class modifyOrderandStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "customers");

            migrationBuilder.RenameColumn(
                name: "StaffName",
                table: "staffs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "staffs",
                newName: "HomeAddress");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Fax",
                table: "customers",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "CIC",
                table: "staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VAT",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CIC",
                table: "staffs");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "staffs",
                newName: "StaffName");

            migrationBuilder.RenameColumn(
                name: "HomeAddress",
                table: "staffs",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "customers",
                newName: "Fax");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
