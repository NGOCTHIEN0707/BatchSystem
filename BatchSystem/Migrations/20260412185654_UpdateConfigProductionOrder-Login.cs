using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigProductionOrderLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductionOrders");

            migrationBuilder.AddColumn<string>(
                name: "CustomerLoginId",
                table: "ProductionOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Logins",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionOrders_CustomerLoginId",
                table: "ProductionOrders",
                column: "CustomerLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionOrders_Logins_CustomerLoginId",
                table: "ProductionOrders",
                column: "CustomerLoginId",
                principalTable: "Logins",
                principalColumn: "LoginId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionOrders_Logins_CustomerLoginId",
                table: "ProductionOrders");

            migrationBuilder.DropIndex(
                name: "IX_ProductionOrders_CustomerLoginId",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "CustomerLoginId",
                table: "ProductionOrders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Logins");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductionOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
