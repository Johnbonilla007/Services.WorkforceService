using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Relationship_Between_ServiceProvider_And_Service_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_ServiceId",
                schema: "Business",
                table: "ServiceProvider",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceProvider_Service_ServiceId",
                schema: "Business",
                table: "ServiceProvider",
                column: "ServiceId",
                principalSchema: "Commons",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceProvider_Service_ServiceId",
                schema: "Business",
                table: "ServiceProvider");

            migrationBuilder.DropIndex(
                name: "IX_ServiceProvider_ServiceId",
                schema: "Business",
                table: "ServiceProvider");
        }
    }
}
