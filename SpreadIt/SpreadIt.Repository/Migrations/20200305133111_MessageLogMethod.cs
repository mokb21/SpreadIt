using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.Repository.Migrations
{
    public partial class MessageLogMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "MessageLogs",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "MessageLogs",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Method",
                table: "MessageLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "MessageLogs",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5000);
        }
    }
}
