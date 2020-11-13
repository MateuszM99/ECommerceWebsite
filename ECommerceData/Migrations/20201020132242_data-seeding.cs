using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceData.Migrations
{
    public partial class dataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Products",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Carts",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "T-shirt" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Jumper" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Longsleeve" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AddedAt", "CategoryId", "Description", "ImageUrl", "Name", "Price", "SKU" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 10, 20, 15, 22, 42, 233, DateTimeKind.Local).AddTicks(9138), 1, "Plain black silk t-shirt", null, "Black t-shirt", 24.989999999999998, "BL-T-1" },
                    { 2, new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(2959), 1, "Plain white silk t-shirt", null, "White t-shirt", 24.989999999999998, "WT-T-2" },
                    { 3, new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3098), 2, "Jumper with logo", null, "Bogo Jumper", 69.989999999999995, "BG-JMP-3" },
                    { 4, new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3105), 2, "Comfortable oversize hoodie", null, "Oversize hoodie", 79.989999999999995, "OS-H-4" },
                    { 5, new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3109), 3, "Longsleeve with white stripes", null, "Grey Stripped Longsleeve", 49.990000000000002, "GRST-LS-2" }
                });

            migrationBuilder.InsertData(
                table: "ProductOption",
                columns: new[] { "OptionId", "ProductId", "ProductStock" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 5, 4, 5 },
                    { 4, 4, 5 },
                    { 5, 3, 5 },
                    { 4, 3, 5 },
                    { 3, 3, 5 },
                    { 2, 3, 5 },
                    { 1, 3, 5 },
                    { 4, 5, 5 },
                    { 5, 2, 5 },
                    { 3, 2, 5 },
                    { 2, 2, 5 },
                    { 1, 2, 5 },
                    { 5, 1, 5 },
                    { 4, 1, 5 },
                    { 3, 1, 5 },
                    { 2, 1, 5 },
                    { 4, 2, 5 },
                    { 5, 5, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "ProductOption",
                keyColumns: new[] { "OptionId", "ProductId" },
                keyValues: new object[] { 5, 5 });

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

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Products",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Carts",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
