using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoDev.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PerformedByMechanicUserId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs");

            migrationBuilder.AlterColumn<int>(
                name: "PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                column: "PerformedByMechanicUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs");

            migrationBuilder.AlterColumn<int>(
                name: "PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientCarRepairs_Users_PerformedByMechanicUserId",
                table: "ClientCarRepairs",
                column: "PerformedByMechanicUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
