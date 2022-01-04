using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Migrations
{
    public partial class changeCustomerIdtoUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "orders",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "CustomerId");
        }
    }
}
