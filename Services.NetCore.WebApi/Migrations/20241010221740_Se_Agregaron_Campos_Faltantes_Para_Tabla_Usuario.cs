using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.NetCore.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Se_Agregaron_Campos_Faltantes_Para_Tabla_Usuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Commons",
                table: "User",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Commons",
                table: "User",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Commons",
                table: "User",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Commons",
                table: "User",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "Commons",
                table: "User",
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
                name: "Email",
                schema: "Commons",
                table: "User_Transactions");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Commons",
                table: "User_Transactions");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Commons",
                table: "User_Transactions");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "Commons",
                table: "User_Transactions");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Commons",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Commons",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Commons",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "Commons",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Commons",
                table: "User_Transactions",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Commons",
                table: "User",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100);
        }
    }
}
