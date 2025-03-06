using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ykotika.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InputRecordsCofiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InputRecord",
                table: "InputRecord");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputRecord",
                table: "InputRecord",
                columns: new[] { "Id", "FormRecordId" });

            migrationBuilder.CreateIndex(
                name: "IX_InputRecord_FormRecordId",
                table: "InputRecord",
                column: "FormRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InputRecord",
                table: "InputRecord");

            migrationBuilder.DropIndex(
                name: "IX_InputRecord_FormRecordId",
                table: "InputRecord");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InputRecord",
                table: "InputRecord",
                columns: new[] { "FormRecordId", "Id" });
        }
    }
}
