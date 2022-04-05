using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class adduniqueUserNameconstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "Unique_UserName_Constraint",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Unique_UserName_Constraint",
                table: "AspNetUsers");
        }
    }
}
