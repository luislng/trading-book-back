using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TradingBook.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CryptoCurrencyReference",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencyReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Deposit = table.Column<decimal>(type: "TEXT", nullable: false, defaultValue: 0.0m),
                    DepositDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockReference",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReference", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CryptoCurrencyReferenceFromId = table.Column<uint>(type: "INTEGER", nullable: false),
                    CryptoCurrencyReferenceToId = table.Column<uint>(type: "INTEGER", nullable: false),
                    AmountInvest = table.Column<decimal>(type: "TEXT", nullable: false),
                    FeeInvest = table.Column<decimal>(type: "TEXT", nullable: false),
                    CryptoPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExchangedAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    IsSelled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ReturnPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SellDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReturnFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    StopLoss = table.Column<decimal>(type: "TEXT", nullable: false),
                    SellLimit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoCurrency_CryptoCurrencyReference_CryptoCurrencyReferenceFromId",
                        column: x => x.CryptoCurrencyReferenceFromId,
                        principalTable: "CryptoCurrencyReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CryptoCurrency_CryptoCurrencyReference_CryptoCurrencyReferenceToId",
                        column: x => x.CryptoCurrencyReferenceToId,
                        principalTable: "CryptoCurrencyReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StockReferenceId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    BuyDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    CurrencyId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Fee = table.Column<decimal>(type: "TEXT", nullable: false),
                    StopLoss = table.Column<decimal>(type: "TEXT", nullable: false),
                    SellLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsSelled = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    ReturnAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReturnFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    ReturnStockPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    SellDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_StockReference_StockReferenceId",
                        column: x => x.StockReferenceId,
                        principalTable: "StockReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CryptoCurrencyReference",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1u, "BTC", "Bitcoin" },
                    { 2u, "ETH", "Ethereum" },
                    { 3u, "EUR", "Eur" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1u, "EUR", "Euro" },
                    { 2u, "USD", "Dollar" }
                });

            migrationBuilder.InsertData(
                table: "StockReference",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1u, "MSFT", "Microsoft" },
                    { 2u, "TSM", "TaiwanSemiCond" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoCurrency_CryptoCurrencyReferenceFromId",
                table: "CryptoCurrency",
                column: "CryptoCurrencyReferenceFromId");

            migrationBuilder.CreateIndex(
                name: "IX_CryptoCurrency_CryptoCurrencyReferenceToId",
                table: "CryptoCurrency",
                column: "CryptoCurrencyReferenceToId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_CurrencyId",
                table: "Stock",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_StockReferenceId",
                table: "Stock",
                column: "StockReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CryptoCurrency");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "CryptoCurrencyReference");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "StockReference");
        }
    }
}
