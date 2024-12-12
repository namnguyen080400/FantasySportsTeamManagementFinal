using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerManagementService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Players",
                newName: "playerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "playerId",
                table: "Players",
                newName: "PlayerId");
        }
    }
}
