using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ServiceProvider_Table_Was_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "Rating",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationDocumentBackPath",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationDocumentFrontPath",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationStatus",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifiedBy",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                schema: "Commons",
                table: "Provider",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                schema: "Commons",
                table: "Provider",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationDocumentBackPath",
                schema: "Commons",
                table: "Provider",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationDocumentFrontPath",
                schema: "Commons",
                table: "Provider",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationStatus",
                schema: "Commons",
                table: "Provider",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifiedBy",
                schema: "Commons",
                table: "Provider",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceProvider",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProvider_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "Commons",
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProvider_Transactions",
                schema: "Business",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProvider_Transactions", x => x.UId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProvider_ProviderId",
                schema: "Business",
                table: "ServiceProvider",
                column: "ProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceProvider",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "ServiceProvider_Transactions",
                schema: "Business");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "VerificationDocumentBackPath",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "VerificationDocumentFrontPath",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "VerifiedBy",
                schema: "Commons",
                table: "Provider_Transactions");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "VerificationDocumentBackPath",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "VerificationDocumentFrontPath",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "VerificationStatus",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "VerifiedBy",
                schema: "Commons",
                table: "Provider");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                schema: "Commons",
                table: "Provider_Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                schema: "Commons",
                table: "Provider",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                schema: "Commons",
                table: "Provider",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
