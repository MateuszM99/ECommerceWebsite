using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceData.Migrations
{
    public partial class seedingNewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[] { 1, "Courier", 14.99 });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Cash on delivery" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2020, 11, 15, 23, 0, 20, 732, DateTimeKind.Local).AddTicks(7479));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedAt",
                value: new DateTime(2020, 11, 15, 23, 0, 20, 735, DateTimeKind.Local).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedAt",
                value: new DateTime(2020, 11, 15, 23, 0, 20, 735, DateTimeKind.Local).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddedAt",
                value: new DateTime(2020, 11, 15, 23, 0, 20, 735, DateTimeKind.Local).AddTicks(154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddedAt",
                value: new DateTime(2020, 11, 15, 23, 0, 20, 735, DateTimeKind.Local).AddTicks(158));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddedAt",
                value: new DateTime(2020, 10, 20, 15, 22, 42, 233, DateTimeKind.Local).AddTicks(9138));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddedAt",
                value: new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(2959));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddedAt",
                value: new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3098));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddedAt",
                value: new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3105));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddedAt",
                value: new DateTime(2020, 10, 20, 15, 22, 42, 237, DateTimeKind.Local).AddTicks(3109));
        }
    }
}
