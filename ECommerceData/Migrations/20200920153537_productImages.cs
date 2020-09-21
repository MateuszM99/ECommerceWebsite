using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerceData.Migrations
{
    public partial class productImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItems",
                newName: "IX_OrderItems_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OptionGroupId",
                table: "Options",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options",
                column: "OptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItem",
                newName: "IX_OrderItem_ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "OptionGroupId",
                table: "Options",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionGroups_OptionGroupId",
                table: "Options",
                column: "OptionGroupId",
                principalTable: "OptionGroups",
                principalColumn: "OptionGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
