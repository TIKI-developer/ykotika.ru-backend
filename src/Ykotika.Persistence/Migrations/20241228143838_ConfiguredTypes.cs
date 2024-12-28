using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ykotika.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutsourceShops_Products_ProductId",
                table: "OutsourceShops");

            migrationBuilder.DropIndex(
                name: "IX_OutsourceShops_ProductId",
                table: "OutsourceShops");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OutsourceShops");

            migrationBuilder.CreateTable(
                name: "OutsourceShopProduct",
                columns: table => new
                {
                    OutsourceShopsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutsourceShopProduct", x => new { x.OutsourceShopsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OutsourceShopProduct_OutsourceShops_OutsourceShopsId",
                        column: x => x.OutsourceShopsId,
                        principalTable: "OutsourceShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OutsourceShopProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceShopProduct_ProductsId",
                table: "OutsourceShopProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutsourceShopProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "OutsourceShops",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutsourceShops_ProductId",
                table: "OutsourceShops",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutsourceShops_Products_ProductId",
                table: "OutsourceShops",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
