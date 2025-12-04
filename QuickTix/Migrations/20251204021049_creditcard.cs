using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTix.Migrations
{
    /// <inheritdoc />
    public partial class creditcard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVV",
                table: "Purchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardNumber",
                table: "Purchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpirationDate",
                table: "Purchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "CreditCardNumber",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Purchase");
        }
    }
}
