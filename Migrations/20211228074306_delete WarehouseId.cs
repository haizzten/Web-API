using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class deleteWarehouseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "items",
                type: "int",
                nullable: true);
        }
    }
}
