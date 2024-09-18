using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class initial_load : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ExceptionHandler");

            migrationBuilder.EnsureSchema(
                name: "Commons");

            migrationBuilder.CreateTable(
                name: "LogExceptions",
                schema: "ExceptionHandler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogExceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Commons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DatePurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestParameter",
                schema: "ExceptionHandler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogExceptionId = table.Column<int>(type: "int", nullable: true),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestParameter_LogExceptions_LogExceptionId",
                        column: x => x.LogExceptionId,
                        principalSchema: "ExceptionHandler",
                        principalTable: "LogExceptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestParameter_LogExceptionId",
                schema: "ExceptionHandler",
                table: "RequestParameter",
                column: "LogExceptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "Commons");

            migrationBuilder.DropTable(
                name: "RequestParameter",
                schema: "ExceptionHandler");

            migrationBuilder.DropTable(
                name: "LogExceptions",
                schema: "ExceptionHandler");
        }
    }
}
