using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Location_Was_Moved_To_ServiceProvider_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Business",
                table: "ServiceProvider_Transactions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Business",
                table: "ServiceProvider_Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
