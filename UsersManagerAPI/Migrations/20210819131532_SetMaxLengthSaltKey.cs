using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManagerAPI.Migrations
{
    public partial class SetMaxLengthSaltKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaltKey",
                table: "Users",
                type: "nvarchar(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SaltKey",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(48)");
        }
    }
}
