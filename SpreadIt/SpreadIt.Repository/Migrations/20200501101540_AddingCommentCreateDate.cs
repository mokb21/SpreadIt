using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.Repository.Migrations
{
    public partial class AddingCommentCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebbd8d93-efa6-4abc-964a-96b632aef92d", "AQAAAAEAACcQAAAAEKI5TfOwHIbvLp60YDGOm+RMRMkrGhuqFcLHnKpHVjXeWBCJaUHd7JAKFatPS98PDA==", "11c0a646-bcff-4765-92e6-1aeb8b08a6fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0d8f08ed-d6f0-4596-b8cf-118907c1bb4a", "AQAAAAEAACcQAAAAEHKSWd/uIeim2nuFD2fqcildJ/VenJZRpWv01sLyq58S2oaKykotM8n7g+ECW/Wc4w==", "bbe43499-eb3d-4b43-ad4a-8e688ef6f224" });

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_ReportCategoryId",
                table: "PostReports",
                column: "ReportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReports_ReportCategoryId",
                table: "CommentReports",
                column: "ReportCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReports_ReportCategories_ReportCategoryId",
                table: "CommentReports",
                column: "ReportCategoryId",
                principalTable: "ReportCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_ReportCategories_ReportCategoryId",
                table: "PostReports",
                column: "ReportCategoryId",
                principalTable: "ReportCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReports_ReportCategories_ReportCategoryId",
                table: "CommentReports");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_ReportCategories_ReportCategoryId",
                table: "PostReports");

            migrationBuilder.DropIndex(
                name: "IX_PostReports_ReportCategoryId",
                table: "PostReports");

            migrationBuilder.DropIndex(
                name: "IX_CommentReports_ReportCategoryId",
                table: "CommentReports");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5d552fd-9091-4897-b3c3-6fd46c2ba10b", "AQAAAAEAACcQAAAAEA9/MEVb9KOS/DVWwWhMkPgiW43BOOu21B6Ir9x54KzAJGI0Wvnsgpmig40HsNB35A==", "dd5683d6-317a-4d28-873b-9e0a184e91e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c5aa8bc-ad01-4574-a3d8-7c8f1f02f07e", "AQAAAAEAACcQAAAAEIb1T/3aqCiEKNdUe57nk0Yjb3g/7oI6GLKboIRJuUpNTLH0ZJKxAngTLax2M1h1MQ==", "3fd461ce-9ffb-47e0-8209-9b563feb568a" });
        }
    }
}
