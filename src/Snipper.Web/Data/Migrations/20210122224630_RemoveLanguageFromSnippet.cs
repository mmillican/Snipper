using Microsoft.EntityFrameworkCore.Migrations;

namespace Snipper.Web.Data.Migrations
{
    public partial class RemoveLanguageFromSnippet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Snippets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Snippets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
