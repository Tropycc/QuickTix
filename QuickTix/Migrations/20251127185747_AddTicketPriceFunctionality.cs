using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickTix.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketPriceFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listing_Purchase_PurchaseId",
                table: "Listing");

            migrationBuilder.DropIndex(
                name: "IX_Listing_PurchaseId",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Listing");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerTicket",
                table: "Purchase",
                type: "decimal(10, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchase",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Purchase",
                type: "decimal(10, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "Listing",
                type: "decimal(10, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ListingId",
                table: "Purchase",
                column: "ListingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_Listing_ListingId",
                table: "Purchase",
                column: "ListingId",
                principalTable: "Listing",
                principalColumn: "ListingId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_Listing_ListingId",
                table: "Purchase");

            migrationBuilder.DropIndex(
                name: "IX_Purchase_ListingId",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "PricePerTicket",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Listing");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Listing",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listing_PurchaseId",
                table: "Listing",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listing_Purchase_PurchaseId",
                table: "Listing",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "PurchaseId");
        }
    }
}
