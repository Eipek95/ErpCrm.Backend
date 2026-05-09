using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpCrm.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId_ProductVariantId_WarehouseId",
                table: "Stocks",
                columns: new[] { "ProductId", "ProductVariantId", "WarehouseId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ReferenceNumber",
                table: "StockMovements",
                column: "ReferenceNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId_ProductVariantId_WarehouseId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ReferenceNumber",
                table: "StockMovements");
        }
    }
}
