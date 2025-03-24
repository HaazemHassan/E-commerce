using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderDetailsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdeRSDetail_Orders_OrderId",
                table: "OrdeRSDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdeRSDetail_Products_ProductId",
                table: "OrdeRSDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdeRSDetail",
                table: "OrdeRSDetail");

            migrationBuilder.RenameTable(
                name: "OrdeRSDetail",
                newName: "OrdersDetail");

            migrationBuilder.RenameIndex(
                name: "IX_OrdeRSDetail_ProductId",
                table: "OrdersDetail",
                newName: "IX_OrdersDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdeRSDetail_OrderId",
                table: "OrdersDetail",
                newName: "IX_OrdersDetail_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDetail",
                table: "OrdersDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetail_Orders_OrderId",
                table: "OrdersDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetail_Products_ProductId",
                table: "OrdersDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetail_Orders_OrderId",
                table: "OrdersDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetail_Products_ProductId",
                table: "OrdersDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDetail",
                table: "OrdersDetail");

            migrationBuilder.RenameTable(
                name: "OrdersDetail",
                newName: "OrdeRSDetail");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersDetail_ProductId",
                table: "OrdeRSDetail",
                newName: "IX_OrdeRSDetail_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersDetail_OrderId",
                table: "OrdeRSDetail",
                newName: "IX_OrdeRSDetail_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdeRSDetail",
                table: "OrdeRSDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdeRSDetail_Orders_OrderId",
                table: "OrdeRSDetail",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdeRSDetail_Products_ProductId",
                table: "OrdeRSDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
