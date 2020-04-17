using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.IdSrv.Data.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class CustomPropertiesSetLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e19a246e-fb60-408a-8fd9-b1564ebed8ca", "AQAAAAEAACcQAAAAEDdm6bTvAdAUIU36k7lKTBeZwweeI+CVeyw5DutNaJPR1mGqASfuRX1gOwfzMLhfag==", "32394983-f6cd-4357-98b2-7f4eeae3401f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e7941b0-1d3c-45db-9c33-8cc47afbfeb7", "AQAAAAEAACcQAAAAEB54Pc5X11pZMaDebZ5ra6KkIgXP5lv6R46NPPfl9Up6uhJtw5wx9NqP8C2Pnq7m8A==", "68271fd3-8042-43d5-a993-b7ffe77ad4f5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63a28acd-9cfd-40ac-ad30-69c01cfcaa04", "AQAAAAEAACcQAAAAEMI/hb+N1Cn9U5E7Vt5MrfXAMUv6RHEI+UzQ+X1vwQQdKc+qDf9ceLbQEZkVDZVWMA==", "1afa9941-26b5-40f7-b917-d50e114bbffe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c32b053-ea26-4a2d-aa00-cb2f7ac8a685", "AQAAAAEAACcQAAAAEGHSNKO6ZsMqPNPIi7iiijYSAaZralxbvOXWyDm97cQcizhST7pzVMmmRY97ld7tzA==", "218b1868-0c83-4da1-8032-4221acf4676c" });
        }
    }
}
