using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace f7.Models.Models.Migrations
{
    public partial class modifyItemandBatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_providers_ProviderId",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_ProviderId",
                table: "batches");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "batches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoodIssueId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoodIssueNoteId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "goodIssueNotes",
                columns: table => new
                {
                    GoodIssueNoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goodIssueNotes", x => x.GoodIssueNoteId);
                    table.ForeignKey(
                        name: "FK_goodIssueNotes_staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_batches_GoodIssueId",
                table: "batches",
                column: "GoodIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_batches_GoodIssueNoteId",
                table: "batches",
                column: "GoodIssueNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_goodIssueNotes_StaffId",
                table: "goodIssueNotes",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_goodIssueNotes_GoodIssueNoteId",
                table: "batches",
                column: "GoodIssueNoteId",
                principalTable: "goodIssueNotes",
                principalColumn: "GoodIssueNoteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_batches_providers_GoodIssueId",
                table: "batches",
                column: "GoodIssueId",
                principalTable: "providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_batches_goodIssueNotes_GoodIssueNoteId",
                table: "batches");

            migrationBuilder.DropForeignKey(
                name: "FK_batches_providers_GoodIssueId",
                table: "batches");

            migrationBuilder.DropTable(
                name: "goodIssueNotes");

            migrationBuilder.DropIndex(
                name: "IX_batches_GoodIssueId",
                table: "batches");

            migrationBuilder.DropIndex(
                name: "IX_batches_GoodIssueNoteId",
                table: "batches");

            migrationBuilder.DropColumn(
                name: "State",
                table: "items");

            migrationBuilder.DropColumn(
                name: "GoodIssueId",
                table: "batches");

            migrationBuilder.DropColumn(
                name: "GoodIssueNoteId",
                table: "batches");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "batches",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_batches_ProviderId",
                table: "batches",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_batches_providers_ProviderId",
                table: "batches",
                column: "ProviderId",
                principalTable: "providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
