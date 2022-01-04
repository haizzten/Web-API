using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Migrations
{
    public partial class removeVATprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatPercent",
                table: "orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VatPercent",
                table: "orders",
                type: "int",
                nullable: true);
        }
    }
}
