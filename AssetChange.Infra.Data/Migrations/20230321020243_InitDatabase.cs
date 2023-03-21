using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetChange.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImportedIn = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Currency = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Symbol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ExchangeName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    InstrumentType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FirstTradeDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegularMarketTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gmtoffset = table.Column<int>(type: "int", nullable: false),
                    Timezone = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    ExchangeTimezoneName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    RegularMarketPrice = table.Column<decimal>(type: "money", nullable: false),
                    ChartPreviousClose = table.Column<decimal>(type: "money", nullable: false),
                    PreviousClose = table.Column<decimal>(type: "money", nullable: false),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    PriceHint = table.Column<int>(type: "int", nullable: false),
                    DataGranularity = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Range = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    ValidRanges = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetTradingDate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OpeningValue = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTradingDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetTradingDate_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentTradingPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(type: "int", nullable: false),
                    Timezone = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Start = table.Column<DateTime>(type: "datetime", nullable: false),
                    End = table.Column<DateTime>(type: "datetime", nullable: false),
                    Gmtoffset = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentTradingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentTradingPeriod_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetTradingDate_AssetId",
                table: "AssetTradingDate",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentTradingPeriod_AssetId",
                table: "CurrentTradingPeriod",
                column: "AssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetTradingDate");

            migrationBuilder.DropTable(
                name: "CurrentTradingPeriod");

            migrationBuilder.DropTable(
                name: "Asset");
        }
    }
}
