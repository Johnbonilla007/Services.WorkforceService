using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Genre_Column_Was_Added_To_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "Commons",
                table: "User_Transactions",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "Commons",
                table: "User",
                newName: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                schema: "Commons",
                table: "User_Transactions",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Genre",
                schema: "Commons",
                table: "User",
                newName: "Location");
        }
    }
}
