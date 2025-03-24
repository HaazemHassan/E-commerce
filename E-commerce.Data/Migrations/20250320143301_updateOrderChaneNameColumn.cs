using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderChaneNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "PersonName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "Orders",
                newName: "Name");
        }
    }
}
