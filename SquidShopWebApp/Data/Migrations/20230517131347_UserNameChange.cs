using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquidShopWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Details", "Name" },
                values: new object[] { 1, "Food from all over the world", "Food" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
