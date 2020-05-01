using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.Repository.Migrations
{
    public partial class RenamingCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
