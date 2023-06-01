using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquidShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateCartAndId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_Fk_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductViewModel_Fk_ProductId",
                table: "CartItems");

            migrationBuilder.DropTable(
                name: "ProductViewModel");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Fk_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_Fk_ProductId",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "CartItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "CartItems");

            migrationBuilder.CreateTable(
                name: "ProductViewModel",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountUnitPrice = table.Column<double>(type: "float", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViewModel", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Fk_CartId",
                table: "CartItems",
                column: "Fk_CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Fk_ProductId",
                table: "CartItems",
                column: "Fk_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_Fk_CartId",
                table: "CartItems",
                column: "Fk_CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductViewModel_Fk_ProductId",
                table: "CartItems",
                column: "Fk_ProductId",
                principalTable: "ProductViewModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
