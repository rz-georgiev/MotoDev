using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamingRepairTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairType_RepairTypeId",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairType",
                table: "RepairType");

            migrationBuilder.RenameTable(
                name: "RepairType",
                newName: "RepairTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairTypes",
                table: "RepairTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairTypes_RepairTypeId",
                table: "ClientCarRepairsDetails",
                column: "RepairTypeId",
                principalTable: "RepairTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairTypes_RepairTypeId",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairTypes",
                table: "RepairTypes");

            migrationBuilder.RenameTable(
                name: "RepairTypes",
                newName: "RepairType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairType",
                table: "RepairType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairType_RepairTypeId",
                table: "ClientCarRepairsDetails",
                column: "RepairTypeId",
                principalTable: "RepairType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
