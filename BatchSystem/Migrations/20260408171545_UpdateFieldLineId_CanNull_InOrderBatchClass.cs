using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldLineId_CanNull_InOrderBatchClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBatchs_Lines_LineId",
                table: "OrderBatchs");

            migrationBuilder.AlterColumn<string>(
                name: "LineId",
                table: "OrderBatchs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBatchs_Lines_LineId",
                table: "OrderBatchs",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "LineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBatchs_Lines_LineId",
                table: "OrderBatchs");

            migrationBuilder.AlterColumn<string>(
                name: "LineId",
                table: "OrderBatchs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBatchs_Lines_LineId",
                table: "OrderBatchs",
                column: "LineId",
                principalTable: "Lines",
                principalColumn: "LineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
