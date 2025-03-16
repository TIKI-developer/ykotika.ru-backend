using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ykotika.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductTypeManualLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManualLink",
                table: "ProductTypes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManualLink",
                table: "ProductTypes");
        }
    }
}
