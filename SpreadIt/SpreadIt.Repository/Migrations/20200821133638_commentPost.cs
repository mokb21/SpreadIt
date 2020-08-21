using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.Repository.Migrations
{
    public partial class commentPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentRates_Comments_CommentId",
                table: "CommentRates");

            migrationBuilder.DropForeignKey(
                name: "FK_PostRates_Posts_PostId",
                table: "PostRates");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostRates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "CommentRates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a401b251-8d0f-4c37-81ed-459dea42abd4", "AQAAAAEAACcQAAAAEDxC23RG49KFCFR2pC/HyEgoTNe2DadwSlm5YoDqCGq8MzBT380Rnp5CsRGvR1psnQ==", "115e2555-a948-469d-9468-d37f08d1fd0a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d2b837d-a092-478d-8cef-a6871d7982dd", "AQAAAAEAACcQAAAAEDa0ahnxqM7xBAo64Su+mUv4rmKywzUAu+r44XOtRsyTyhCGBxq3gwTIXm2Yiy/Hcg==", "485f94c7-c4e2-409b-95a4-0e342c806d5c" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRates_Comments_CommentId",
                table: "CommentRates",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostRates_Posts_PostId",
                table: "PostRates",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentRates_Comments_CommentId",
                table: "CommentRates");

            migrationBuilder.DropForeignKey(
                name: "FK_PostRates_Posts_PostId",
                table: "PostRates");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "PostRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "CommentRates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e62b0609-d983-4de3-99e6-361ce1e9e39c", "AQAAAAEAACcQAAAAEMz5fTw/uAkCAiFvE0uU40onqRa8Q4IELAkYdN5sFFU3sGmEm3heTKzSIsyDaKV4WQ==", "12773ec5-0b57-41b9-9412-4f92a355f298" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd397da2-14d4-43bf-b973-589586d9b9d8", "AQAAAAEAACcQAAAAEI0G+hYodys3KhlhQRGdVjVu50OBNA9cYzibToLvpJXkFGRk6RJfkKvQpeVKScDg4A==", "8f2eee1a-03e3-4d55-b970-56510e6969d3" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRates_Comments_CommentId",
                table: "CommentRates",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostRates_Posts_PostId",
                table: "PostRates",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
