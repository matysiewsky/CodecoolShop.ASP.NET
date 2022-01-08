using Microsoft.EntityFrameworkCore.Migrations;

namespace Codecool.CodecoolShop.Migrations
{
    public partial class SeedingProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Currency", "DefaultPrice", "Description", "Name", "ProductCategoryId", "SupplierId" },
                values: new object[] { 1, "USD", 49.899999999999999, "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", "Amazon Fire", 1, 2 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Currency", "DefaultPrice", "Description", "Name", "ProductCategoryId", "SupplierId" },
                values: new object[] { 2, "USD", 479.0, "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", "Lenovo IdeaPad Miix 700", 1, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Currency", "DefaultPrice", "Description", "Name", "ProductCategoryId", "SupplierId" },
                values: new object[] { 3, "USD", 89.0, "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", "Amazon Fire HD 8", 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
