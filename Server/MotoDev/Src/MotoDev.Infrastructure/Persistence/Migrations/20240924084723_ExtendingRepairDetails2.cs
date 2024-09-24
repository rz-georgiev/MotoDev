using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExtendingRepairDetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RepairEndDateTime",
                table: "ClientCarRepairsDetails",
                type: "datetime(6)",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RepairStartDateTime",
                table: "ClientCarRepairsDetails",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RepairStatusId",
                table: "ClientCarRepairsDetails",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairsDetails_RepairStatusId",
                table: "ClientCarRepairsDetails",
                column: "RepairStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairStatuses_RepairStatusId",
                table: "ClientCarRepairsDetails",
                column: "RepairStatusId",
                principalTable: "RepairStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairsDetails_RepairStatuses_RepairStatusId",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropIndex(
                name: "IX_ClientCarRepairsDetails_RepairStatusId",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropColumn(
                name: "RepairEndDateTime",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropColumn(
                name: "RepairStartDateTime",
                table: "ClientCarRepairsDetails");

            migrationBuilder.DropColumn(
                name: "RepairStatusId",
                table: "ClientCarRepairsDetails");
        }
    }
}
