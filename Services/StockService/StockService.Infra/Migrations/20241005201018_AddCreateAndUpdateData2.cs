using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockService.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateAndUpdateData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.CreateTable(
                name: "TransactionStock",
                columns: table => new
                {
                    TransactionStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvestmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOperationInvestment = table.Column<byte>(type: "tinyint", nullable: false),
                    StatusTransactionStock = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStock", x => x.TransactionStockId);
                    table.ForeignKey(
                        name: "FK_TransactionStock_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionStock_StockId",
                table: "TransactionStock",
                column: "StockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionStock");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionStockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOperationInvestment = table.Column<byte>(type: "tinyint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionStockId);
                    table.ForeignKey(
                        name: "FK_Transactions_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "StockId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StockId",
                table: "Transactions",
                column: "StockId");
        }
    }
}
