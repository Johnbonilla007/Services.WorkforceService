using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UserTable_Was_Added_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UserProfile");

            migrationBuilder.RenameTable(
                name: "Address_Transactions",
                schema: "User",
                newName: "Address_Transactions",
                newSchema: "UserProfile");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "User",
                newName: "Address",
                newSchema: "UserProfile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.RenameTable(
                name: "Address_Transactions",
                schema: "UserProfile",
                newName: "Address_Transactions",
                newSchema: "User");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "UserProfile",
                newName: "Address",
                newSchema: "User");
        }
    }
}
