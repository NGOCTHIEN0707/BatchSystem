using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    LineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.MaterialId);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrders",
                columns: table => new
                {
                    ProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlannedStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlannedEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrders", x => x.ProductionOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "StationTypes",
                columns: table => new
                {
                    StationTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    StationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationTypes", x => x.StationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LineCurrentStatuses",
                columns: table => new
                {
                    LineCurrentStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "LineStatusHistories",
                columns: table => new
                {
                    LineStatusHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Mode = table.Column<int>(type: "int", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineStatusHistories", x => x.LineStatusHistoryId);
                    table.ForeignKey(
                        name: "FK_LineStatusHistories_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    StationTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Stations_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stations_StationTypes_StationTypeId",
                        column: x => x.StationTypeId,
                        principalTable: "StationTypes",
                        principalColumn: "StationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionOrderDetails",
                columns: table => new
                {
                    ProductionOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchQuantity = table.Column<int>(type: "int", nullable: false),
                    SequenceNo = table.Column<int>(type: "int", nullable: false),
                    RecipeSnapshotJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionOrderDetails", x => x.ProductionOrderDetailId);
                    table.ForeignKey(
                        name: "FK_ProductionOrderDetails_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductionOrderDetails_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMaterials",
                columns: table => new
                {
                    RecipeMaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    RecipeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ToleranceMinKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ToleranceMaxKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMaterials", x => x.RecipeMaterialId);
                    table.ForeignKey(
                        name: "FK_RecipeMaterials_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMaterials_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlarmDefinitions",
                columns: table => new
                {
                    AlarmDefinitionId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AlarmText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlarmClass = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmDefinitions", x => x.AlarmDefinitionId);
                    table.ForeignKey(
                        name: "FK_AlarmDefinitions_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBatchs",
                columns: table => new
                {
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductionOrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatchNo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CurrentStationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBatchs", x => x.OrderBatchId);
                    table.ForeignKey(
                        name: "FK_OrderBatchs_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBatchs_ProductionOrderDetails_ProductionOrderDetailId",
                        column: x => x.ProductionOrderDetailId,
                        principalTable: "ProductionOrderDetails",
                        principalColumn: "ProductionOrderDetailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderBatchs_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBatchs_Stations_CurrentStationId",
                        column: x => x.CurrentStationId,
                        principalTable: "Stations",
                        principalColumn: "StationId");
                });

            migrationBuilder.CreateTable(
                name: "AlarmEvents",
                columns: table => new
                {
                    AlarmEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AlarmDefinitionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AckAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AckBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAcked = table.Column<bool>(type: "bit", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmEvents", x => x.AlarmEventId);
                    table.ForeignKey(
                        name: "FK_AlarmEvents_AlarmDefinitions_AlarmDefinitionId",
                        column: x => x.AlarmDefinitionId,
                        principalTable: "AlarmDefinitions",
                        principalColumn: "AlarmDefinitionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlarmEvents_OrderBatchs_OrderBatchId",
                        column: x => x.OrderBatchId,
                        principalTable: "OrderBatchs",
                        principalColumn: "OrderBatchId");
                    table.ForeignKey(
                        name: "FK_AlarmEvents_ProductionOrders_ProductionOrderId",
                        column: x => x.ProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId");
                    table.ForeignKey(
                        name: "FK_AlarmEvents_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BatchWeighingResults",
                columns: table => new
                {
                    BatchWeighingResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TargetKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ActualKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    DeviationKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ToleranceMinKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ToleranceMaxKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    IsWithinTolerance = table.Column<bool>(type: "bit", nullable: false),
                    CapturedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchWeighingResults", x => x.BatchWeighingResultId);
                    table.ForeignKey(
                        name: "FK_BatchWeighingResults_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchWeighingResults_OrderBatchs_OrderBatchId",
                        column: x => x.OrderBatchId,
                        principalTable: "OrderBatchs",
                        principalColumn: "OrderBatchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderBatchSteps",
                columns: table => new
                {
                    OrderBatchStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    OrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StepNo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EnteredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "StationCurrentStatuses",
                columns: table => new
                {
                    StationCurrentStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CurrentBatchId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentOrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentBatchOrderBatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentOrderProductionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationCurrentStatuses", x => x.StationCurrentStatusId);
                    table.ForeignKey(
                        name: "FK_StationCurrentStatuses_OrderBatchs_CurrentBatchOrderBatchId",
                        column: x => x.CurrentBatchOrderBatchId,
                        principalTable: "OrderBatchs",
                        principalColumn: "OrderBatchId");
                    table.ForeignKey(
                        name: "FK_StationCurrentStatuses_ProductionOrders_CurrentOrderProductionOrderId",
                        column: x => x.CurrentOrderProductionOrderId,
                        principalTable: "ProductionOrders",
                        principalColumn: "ProductionOrderId");
                    table.ForeignKey(
                        name: "FK_StationCurrentStatuses_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmDefinitions_StationId",
                table: "AlarmDefinitions",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_AlarmDefinitionId",
                table: "AlarmEvents",
                column: "AlarmDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_OrderBatchId_OccurredAt",
                table: "AlarmEvents",
                columns: new[] { "OrderBatchId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_ProductionOrderId",
                table: "AlarmEvents",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmEvents_StationId",
                table: "AlarmEvents",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchWeighingResults_MaterialId",
                table: "BatchWeighingResults",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchWeighingResults_OrderBatchId_CapturedAt",
                table: "BatchWeighingResults",
                columns: new[] { "OrderBatchId", "CapturedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_LineCurrentStatuses_LineId",
                table: "LineCurrentStatuses",
                column: "LineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineStatusHistories_LineId",
                table: "LineStatusHistories",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchs_CurrentStationId",
                table: "OrderBatchs",
                column: "CurrentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchs_LineId",
                table: "OrderBatchs",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchs_ProductionOrderDetailId",
                table: "OrderBatchs",
                column: "ProductionOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchs_ProductionOrderId",
                table: "OrderBatchs",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchSteps_OrderBatchId",
                table: "OrderBatchSteps",
                column: "OrderBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBatchSteps_StationId",
                table: "OrderBatchSteps",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderDetails_ProductId",
                table: "ProductionOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderDetails_ProductionOrderId",
                table: "ProductionOrderDetails",
                column: "ProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrderDetails_RecipeId",
                table: "ProductionOrderDetails",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMaterials_MaterialId",
                table: "RecipeMaterials",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMaterials_RecipeId",
                table: "RecipeMaterials",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ProductId",
                table: "Recipes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StationCurrentStatuses_CurrentBatchOrderBatchId",
                table: "StationCurrentStatuses",
                column: "CurrentBatchOrderBatchId");

            migrationBuilder.CreateIndex(
                name: "IX_StationCurrentStatuses_CurrentOrderProductionOrderId",
                table: "StationCurrentStatuses",
                column: "CurrentOrderProductionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StationCurrentStatuses_StationId",
                table: "StationCurrentStatuses",
                column: "StationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stations_LineId",
                table: "Stations",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationTypeId",
                table: "Stations",
                column: "StationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmEvents");

            migrationBuilder.DropTable(
                name: "BatchWeighingResults");

            migrationBuilder.DropTable(
                name: "LineCurrentStatuses");

            migrationBuilder.DropTable(
                name: "LineStatusHistories");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "OrderBatchSteps");

            migrationBuilder.DropTable(
                name: "RecipeMaterials");

            migrationBuilder.DropTable(
                name: "StationCurrentStatuses");

            migrationBuilder.DropTable(
                name: "AlarmDefinitions");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "OrderBatchs");

            migrationBuilder.DropTable(
                name: "ProductionOrderDetails");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "ProductionOrders");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "StationTypes");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
