using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RevisionAndAlteringOfAllDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RepairType_RepairTypeId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairTypeId",
                table: "Repairs");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Repairs",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RepairShops",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "RepairShops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "RepairShops",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedByUserId",
                table: "RepairShops",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RepairShops",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "RepairShops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepairShopId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherModifications",
                table: "ClientCars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "VinNumber",
                table: "ClientCars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientCarRepair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RepairId = table.Column<int>(type: "int", nullable: false),
                    RepairTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCarRepair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCarRepair_RepairType_RepairTypeId",
                        column: x => x.RepairTypeId,
                        principalTable: "RepairType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCarRepair_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShops_OwnerUserId",
                table: "RepairShops",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RepairShopId",
                table: "Clients",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepair_RepairId",
                table: "ClientCarRepair",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepair_RepairTypeId",
                table: "ClientCarRepair",
                column: "RepairTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_RepairShops_RepairShopId",
                table: "Clients",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShops_Users_OwnerUserId",
                table: "RepairShops",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_RepairShops_RepairShopId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShops_Users_OwnerUserId",
                table: "RepairShops");

            migrationBuilder.DropTable(
                name: "ClientCarRepair");

            migrationBuilder.DropIndex(
                name: "IX_RepairShops_OwnerUserId",
                table: "RepairShops");

            migrationBuilder.DropIndex(
                name: "IX_Clients_RepairShopId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ClientId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "RepairShops");

            migrationBuilder.DropColumn(
                name: "RepairShopId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OtherModifications",
                table: "ClientCars");

            migrationBuilder.DropColumn(
                name: "VinNumber",
                table: "ClientCars");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Repairs",
                newName: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairTypeId",
                table: "Repairs",
                column: "RepairTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RepairType_RepairTypeId",
                table: "Repairs",
                column: "RepairTypeId",
                principalTable: "RepairType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
