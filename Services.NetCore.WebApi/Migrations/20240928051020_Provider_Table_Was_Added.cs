using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Provider_Table_Was_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provider",
                schema: "Commons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provider_Transactions",
                schema: "Commons",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Provider_Transactions", x => x.UId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provider",
                schema: "Commons");

            migrationBuilder.DropTable(
                name: "Provider_Transactions",
                schema: "Commons");
        }
    }
}
