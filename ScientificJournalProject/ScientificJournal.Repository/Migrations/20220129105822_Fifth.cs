using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificJournal.Repository.Migrations
{
    public partial class Fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfDocumentName",
                table: "Papers");

            migrationBuilder.AddColumn<Guid>(
                name: "PaperDocumentId",
                table: "Papers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaperDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DocumentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaperDocument", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Papers_PaperDocumentId",
                table: "Papers",
                column: "PaperDocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_PaperDocument_PaperDocumentId",
                table: "Papers",
                column: "PaperDocumentId",
                principalTable: "PaperDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_PaperDocument_PaperDocumentId",
                table: "Papers");

            migrationBuilder.DropTable(
                name: "PaperDocument");

            migrationBuilder.DropIndex(
                name: "IX_Papers_PaperDocumentId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "PaperDocumentId",
                table: "Papers");

            migrationBuilder.AddColumn<string>(
                name: "PdfDocumentName",
                table: "Papers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
