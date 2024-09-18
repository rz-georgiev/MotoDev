using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedRepairStatuses4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepairStatusId",
                table: "ClientCarRepairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RepairStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairStatuses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairs_RepairStatusId",
                table: "ClientCarRepairs",
                column: "RepairStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_RepairStatuses_RepairStatusId",
                table: "ClientCarRepairs",
                column: "RepairStatusId",
                principalTable: "RepairStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_RepairStatuses_RepairStatusId",
                table: "ClientCarRepairs");

            migrationBuilder.DropTable(
                name: "RepairStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ClientCarRepairs_RepairStatusId",
                table: "ClientCarRepairs");

            migrationBuilder.DropColumn(
                name: "RepairStatusId",
                table: "ClientCarRepairs");
        }
    }
}
