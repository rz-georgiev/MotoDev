using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PerformedByMechanicUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientCarRepairs_PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                column: "PerformedByMechanicUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                column: "PerformedByMechanicUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs");

            migrationBuilder.DropIndex(
                name: "IX_ClientCarRepairs_PerformedByMechanicUserId",
                table: "ClientCarRepairs");

            migrationBuilder.DropColumn(
                name: "PerformedByMechanicUserId",
                table: "ClientCarRepairs");
        }
    }
}
