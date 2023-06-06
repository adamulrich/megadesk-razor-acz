using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk_Razor_ACZ.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BasePrice = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionSpeedCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    TierAPrice = table.Column<double>(type: "REAL", nullable: true),
                    TierBPrice = table.Column<double>(type: "REAL", nullable: true),
                    TierCPrice = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionSpeedCost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Depth = table.Column<int>(type: "INTEGER", nullable: false),
                    DrawerCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desk_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeskQuote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    ProductionSpeedCostId = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    DeskId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeskQuote_Desk_DeskId",
                        column: x => x.DeskId,
                        principalTable: "Desk",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeskQuote_ProductionSpeedCost_ProductionSpeedCostId",
                        column: x => x.ProductionSpeedCostId,
                        principalTable: "ProductionSpeedCost",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desk_MaterialId",
                table: "Desk",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_ProductionSpeedCostId",
                table: "DeskQuote",
                column: "ProductionSpeedCostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskQuote");

            migrationBuilder.DropTable(
                name: "Desk");

            migrationBuilder.DropTable(
                name: "ProductionSpeedCost");

            migrationBuilder.DropTable(
                name: "Material");
        }
    }
}
