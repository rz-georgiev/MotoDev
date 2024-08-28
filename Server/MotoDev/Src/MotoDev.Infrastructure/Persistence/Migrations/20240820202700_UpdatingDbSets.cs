using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brand_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Model_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepair_RepairType_RepairTypeId",
                table: "ClientCarRepair");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepair_Repairs_RepairId",
                table: "ClientCarRepair");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_Brand_BrandId",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShopUser_RepairShops_RepairShopId",
                table: "RepairShopUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShopUser_Users_UserId",
                table: "RepairShopUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairShopUser",
                table: "RepairShopUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                table: "Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientCarRepair",
                table: "ClientCarRepair");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "RepairShopUser",
                newName: "RepairShopUsers");

            migrationBuilder.RenameTable(
                name: "Model",
                newName: "Models");

            migrationBuilder.RenameTable(
                name: "ClientCarRepair",
                newName: "ClientCarRepairs");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShopUser_UserId",
                table: "RepairShopUsers",
                newName: "IX_RepairShopUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShopUser_RepairShopId",
                table: "RepairShopUsers",
                newName: "IX_RepairShopUsers_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Model_BrandId",
                table: "Models",
                newName: "IX_Models_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepair_RepairTypeId",
                table: "ClientCarRepairs",
                newName: "IX_ClientCarRepairs_RepairTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepair_RepairId",
                table: "ClientCarRepairs",
                newName: "IX_ClientCarRepairs_RepairId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairShopUsers",
                table: "RepairShopUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientCarRepairs",
                table: "ClientCarRepairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BrandModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandModels_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandModels_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RepairShopClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RepairShopId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairShopClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairShopClients_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairShopClients_RepairShops_RepairShopId",
                        column: x => x.RepairShopId,
                        principalTable: "RepairShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRepairShops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RepairShopId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRepairShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRepairShops_RepairShops_RepairShopId",
                        column: x => x.RepairShopId,
                        principalTable: "RepairShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRepairShops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BrandModels_BrandId",
                table: "BrandModels",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandModels_ModelId",
                table: "BrandModels",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShopClients_ClientId",
                table: "RepairShopClients",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairShopClients_RepairShopId",
                table: "RepairShopClients",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRepairShops_RepairShopId",
                table: "UserRepairShops",
                column: "RepairShopId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRepairShops_UserId",
                table: "UserRepairShops",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShopUsers_RepairShops_RepairShopId",
                table: "RepairShopUsers",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShopUsers_Users_UserId",
                table: "RepairShopUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Models_ModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_RepairType_RepairTypeId",
                table: "ClientCarRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_Repairs_RepairId",
                table: "ClientCarRepairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShopUsers_RepairShops_RepairShopId",
                table: "RepairShopUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairShopUsers_Users_UserId",
                table: "RepairShopUsers");

            migrationBuilder.DropTable(
                name: "BrandModels");

            migrationBuilder.DropTable(
                name: "RepairShopClients");

            migrationBuilder.DropTable(
                name: "UserRepairShops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairShopUsers",
                table: "RepairShopUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientCarRepairs",
                table: "ClientCarRepairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "RepairShopUsers",
                newName: "RepairShopUser");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model");

            migrationBuilder.RenameTable(
                name: "ClientCarRepairs",
                newName: "ClientCarRepair");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShopUsers_UserId",
                table: "RepairShopUser",
                newName: "IX_RepairShopUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairShopUsers_RepairShopId",
                table: "RepairShopUser",
                newName: "IX_RepairShopUser_RepairShopId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_BrandId",
                table: "Model",
                newName: "IX_Model_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepairs_RepairTypeId",
                table: "ClientCarRepair",
                newName: "IX_ClientCarRepair_RepairTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientCarRepairs_RepairId",
                table: "ClientCarRepair",
                newName: "IX_ClientCarRepair_RepairId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairShopUser",
                table: "RepairShopUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                table: "Model",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientCarRepair",
                table: "ClientCarRepair",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brand_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Model_ModelId",
                table: "Cars",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepair_RepairType_RepairTypeId",
                table: "ClientCarRepair",
                column: "RepairTypeId",
                principalTable: "RepairType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepair_Repairs_RepairId",
                table: "ClientCarRepair",
                column: "RepairId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Brand_BrandId",
                table: "Model",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShopUser_RepairShops_RepairShopId",
                table: "RepairShopUser",
                column: "RepairShopId",
                principalTable: "RepairShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairShopUser_Users_UserId",
                table: "RepairShopUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
