using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ClientCarRepairsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_RepairType_RepairTypeId",
                table: "ClientCarRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_Repairs_RepairId",
                table: "ClientCarRepairs");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_ClientCarRepairs_RepairTypeId",
                table: "ClientCarRepairs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ClientCarRepairs");

            migrationBuilder.RenameColumn(
                name: "RepairTypeId",
                table: "ClientCarRepairs",
                newName: "LastKilometers");

            migrationBuilder.RenameColumn(
                name: "RepairId",
                table: "ClientCarRepairs",
                newName: "ClientCarId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepairs_RepairId",
                table: "ClientCarRepairs",
                newName: "IX_ClientCarRepairs_ClientCarId");

            migrationBuilder.CreateTable(
                name: "ClientCarRepairsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientCarRepairId = table.Column<int>(type: "int", nullable: false),
                    RepairTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCarRepairsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCarRepairsDetails_ClientCarRepairs_ClientCarRepairId",
                        column: x => x.ClientCarRepairId,
                        principalTable: "ClientCarRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCarRepairsDetails_RepairType_RepairTypeId",
                        column: x => x.RepairTypeId,
                        principalTable: "RepairType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairsDetails_ClientCarRepairId",
                table: "ClientCarRepairsDetails",
                column: "ClientCarRepairId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairsDetails_RepairTypeId",
                table: "ClientCarRepairsDetails",
                column: "RepairTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_ClientCars_ClientCarId",
                table: "ClientCarRepairs",
                column: "ClientCarId",
                principalTable: "ClientCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_ClientCars_ClientCarId",
                table: "ClientCarRepairs");

            migrationBuilder.DropTable(
                name: "ClientCarRepairsDetails");

            migrationBuilder.RenameColumn(
                name: "LastKilometers",
                table: "ClientCarRepairs",
                newName: "RepairTypeId");

            migrationBuilder.RenameColumn(
                name: "ClientCarId",
                table: "ClientCarRepairs",
                newName: "RepairId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepairs_ClientCarId",
                table: "ClientCarRepairs",
                newName: "IX_ClientCarRepairs_RepairId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ClientCarRepairs",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientCarId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    LastKilometers = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastUpdatedByUserId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RepairTypeId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_ClientCars_ClientCarId",
                        column: x => x.ClientCarId,
                        principalTable: "ClientCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairs_RepairTypeId",
                table: "ClientCarRepairs",
                column: "RepairTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ClientCarId",
                table: "Repairs",
                column: "ClientCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_RepairType_RepairTypeId",
                table: "ClientCarRepairs",
                column: "RepairTypeId",
                principalTable: "RepairType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_Repairs_RepairId",
                table: "ClientCarRepairs",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
