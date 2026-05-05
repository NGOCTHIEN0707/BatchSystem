using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHistoryForOrderBatchAndProductionOrder_DeleteLineCurrentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineCurrentStatuses");

            migrationBuilder.RenameColumn(
                name: "BatchQuantity",
                table: "ProductionOrderDetails",
                newName: "NumberOfPieces");

            migrationBuilder.AlterColumn<int>(
                name: "WeightPerPieceKg",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ToleranceMinKg",
                table: "BatchWeighingResults",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<float>(
                name: "ToleranceMaxKg",
                table: "BatchWeighingResults",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<float>(
                name: "TargetKg",
                table: "BatchWeighingResults",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<float>(
                name: "DeviationKg",
                table: "BatchWeighingResults",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<float>(
                name: "ActualKg",
                table: "BatchWeighingResults",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.CreateTable(
                name: "OrderBatchStatusHistories",
                columns: table => new
                {
                    OrderBatchStatusHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBatchStatusHistories", x => x.OrderBatchStatusHistoryId);
                    table.ForeignKey(
                        name: "FK_OrderBatchStatusHistories_OrderBatchs_OrderBatchId",
                        column: x => x.OrderBatchId,
                        principalTable: "OrderBatchs",
                        principalColumn: "OrderBatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrderStatusHistories",
                columns: table => new
                {
                    ProductionOrderStatusHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PreviousStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrderStatusHistories", x => x.ProductionOrderStatusHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductionOrderStatusHistories_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchStatusHistories_OrderBatchId",
                table: "OrderBatchStatusHistories",
                column: "OrderBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderStatusHistories_ProductionOrderId",
                table: "ProductionOrderStatusHistories",
                column: "ProductionOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBatchStatusHistories");

            migrationBuilder.DropTable(
                name: "ProductionOrderStatusHistories");

            migrationBuilder.RenameColumn(
                name: "NumberOfPieces",
                table: "ProductionOrderDetails",
                newName: "BatchQuantity");

            migrationBuilder.AlterColumn<int>(
                name: "WeightPerPieceKg",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "ToleranceMinKg",
                table: "BatchWeighingResults",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "ToleranceMaxKg",
                table: "BatchWeighingResults",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "TargetKg",
                table: "BatchWeighingResults",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "DeviationKg",
                table: "BatchWeighingResults",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "ActualKg",
                table: "BatchWeighingResults",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateTable(
                name: "LineCurrentStatuses",
                columns: table => new
                {
                    LineCurrentStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineCurrentStatuses", x => x.LineCurrentStatusId);
                    table.ForeignKey(
                        name: "FK_LineCurrentStatuses_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineCurrentStatuses_LineId",
                table: "LineCurrentStatuses",
                column: "LineId",
                unique: true);
        }
    }
}
