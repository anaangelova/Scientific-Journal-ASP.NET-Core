using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificJournal.Repository.Migrations
{
    public partial class AddedNewEntityConference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConferenceId",
                table: "Papers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConferenceName = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Papers_ConferenceId",
                table: "Papers",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_Conferences_ConferenceId",
                table: "Papers",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_Conferences_ConferenceId",
                table: "Papers");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropIndex(
                name: "IX_Papers_ConferenceId",
                table: "Papers");

            migrationBuilder.DropColumn(
                name: "ConferenceId",
                table: "Papers");
        }
    }
}
