using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorRepoUI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Items");
        }
    }
}
