using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk_Razor_ACZ.Migrations
{
    /// <inheritdoc />
    public partial class addids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeskQuote_ProductionSpeedCost_ProductionSpeedCostId",
                table: "DeskQuote");

            migrationBuilder.AlterColumn<double>(
                name: "TierCPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TierBPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TierAPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductionSpeedCostId",
                table: "DeskQuote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeskQuote_ProductionSpeedCost_ProductionSpeedCostId",
                table: "DeskQuote",
                column: "ProductionSpeedCostId",
                principalTable: "ProductionSpeedCost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeskQuote_ProductionSpeedCost_ProductionSpeedCostId",
                table: "DeskQuote");

            migrationBuilder.AlterColumn<double>(
                name: "TierCPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<double>(
                name: "TierBPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<double>(
                name: "TierAPrice",
                table: "ProductionSpeedCost",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "ProductionSpeedCostId",
                table: "DeskQuote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_DeskQuote_ProductionSpeedCost_ProductionSpeedCostId",
                table: "DeskQuote",
                column: "ProductionSpeedCostId",
                principalTable: "ProductionSpeedCost",
                principalColumn: "Id");
        }
    }
}
