using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class User_Was_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Commons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_Transactions",
                schema: "Commons",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
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
                    table.PrimaryKey("PK_User_Transactions", x => x.UId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User",
                schema: "Commons");

            migrationBuilder.DropTable(
                name: "User_Transactions",
                schema: "Commons");

            migrationBuilder.EnsureSchema(
                name: "ExceptionHandler");

            migrationBuilder.CreateTable(
                name: "LogExceptions",
                schema: "ExceptionHandler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExceptionError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePurchase = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    LogExceptionId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TransactionUId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
