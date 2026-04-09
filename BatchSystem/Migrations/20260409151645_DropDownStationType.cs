using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatchSystem.Migrations
{
    /// <inheritdoc />
    public partial class DropDownStationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stations_StationTypes_StationTypeId",
                table: "Stations");

            migrationBuilder.DropTable(
                name: "StationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Stations_StationTypeId",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "StationTypeId",
                table: "Stations");

            migrationBuilder.AddColumn<string>(
                name: "StationCode",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StationCode",
                table: "Stations");

            migrationBuilder.AddColumn<string>(
                name: "StationTypeId",
                table: "Stations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationTypeId",
                table: "Stations",
                column: "StationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stations_StationTypes_StationTypeId",
                table: "Stations",
                column: "StationTypeId",
                principalTable: "StationTypes",
                principalColumn: "StationTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
