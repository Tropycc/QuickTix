using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTix.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Listing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Listing",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Listing",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
