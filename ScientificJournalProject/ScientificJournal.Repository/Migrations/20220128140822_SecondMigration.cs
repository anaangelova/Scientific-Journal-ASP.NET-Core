using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificJournal.Repository.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Papers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    AreaOfResearch = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PapersKeywords",
                columns: table => new
                {
                    PaperId = table.Column<Guid>(nullable: false),
                    Keyword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapersKeywords", x => new { x.PaperId, x.Keyword });
                    table.ForeignKey(
                        name: "FK_PapersKeywords_Papers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Papers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PapersUsers",
                columns: table => new
                {
                    PaperId = table.Column<Guid>(nullable: false),
                    ScienceUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapersUsers", x => new { x.PaperId, x.ScienceUserId });
                    table.ForeignKey(
                        name: "FK_PapersUsers_Papers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Papers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PapersUsers_AspNetUsers_ScienceUserId",
                        column: x => x.ScienceUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PapersUsers_ScienceUserId",
                table: "PapersUsers",
                column: "ScienceUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PapersKeywords");

            migrationBuilder.DropTable(
                name: "PapersUsers");

            migrationBuilder.DropTable(
                name: "Papers");
        }
    }
}
