using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquidShopApi.Migrations
{
    /// <inheritdoc />
    public partial class testDataToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Discount", "DiscountUnitPrice", "FK_CategoryId", "IMG", "InStock", "ProductName", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 0m, 0.0, 1, "No URL", 10, "Jonny Boy", 199.0 },
                    { 2, 0m, 0.0, 1, "No URL", 29, "After the laughter comes tears", 149.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);
        }
    }
}
