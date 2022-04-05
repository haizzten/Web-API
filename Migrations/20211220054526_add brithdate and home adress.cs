using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class addbrithdateandhomeadress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeAdress",
                table: "AspNetUsers",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HomeAdress",
                table: "AspNetUsers");
        }
    }
}
