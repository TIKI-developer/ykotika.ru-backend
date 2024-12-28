using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ykotika.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticlePattern",
                table: "ProductTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Article",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticlePattern",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Article",
                table: "Products");
        }
    }
}
