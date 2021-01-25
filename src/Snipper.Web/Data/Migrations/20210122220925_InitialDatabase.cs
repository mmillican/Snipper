using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Snipper.Web.Data.Migrations
{
    public partial class InitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Slug);
                });

            migrationBuilder.CreateTable(
                name: "Snippets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategorySlug = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snippets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snippets_Categories_CategorySlug",
                        column: x => x.CategorySlug,
                        principalTable: "Categories",
                        principalColumn: "Slug",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SnippetFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SnippetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnippetFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SnippetFiles_Snippets_SnippetId",
                        column: x => x.SnippetId,
                        principalTable: "Snippets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SnippetFiles_SnippetId",
                table: "SnippetFiles",
                column: "SnippetId");

            migrationBuilder.CreateIndex(
                name: "IX_Snippets_CategorySlug",
                table: "Snippets",
                column: "CategorySlug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnippetFiles");

            migrationBuilder.DropTable(
                name: "Snippets");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
