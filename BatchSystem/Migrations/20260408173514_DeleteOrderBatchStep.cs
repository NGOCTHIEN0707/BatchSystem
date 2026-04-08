using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class DeleteOrderBatchStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBatchSteps");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStep",
                table: "OrderBatchs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStep",
                table: "OrderBatchs");

            migrationBuilder.CreateTable(
                name: "OrderBatchSteps",
                columns: table => new
                {
                    OrderBatchStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnteredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StepNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBatchSteps", x => x.OrderBatchStepId);
                    table.ForeignKey(
                        name: "FK_OrderBatchSteps_OrderBatchs_OrderBatchId",
                        column: x => x.OrderBatchId,
                        principalTable: "OrderBatchs",
                        principalColumn: "OrderBatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBatchSteps_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchSteps_OrderBatchId",
                table: "OrderBatchSteps",
                column: "OrderBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchSteps_StationId",
                table: "OrderBatchSteps",
                column: "StationId");
        }
    }
}
