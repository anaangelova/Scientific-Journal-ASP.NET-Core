using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificJournal.Repository.Migrations
{
    public partial class FourthAddedPropertyInPaperModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfDocumentName",
                table: "Papers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfDocumentName",
                table: "Papers");
        }
    }
}
