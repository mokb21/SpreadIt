using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.IdSrv.Data.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class IdentityUserCustomProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Range = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLocation",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLocation", x => new { x.UserId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_UserLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLocation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserLocation_LocationId",
                table: "UserLocation",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLocation");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "859cb4b2-73a9-4f36-9e16-0a6029794862", "AQAAAAEAACcQAAAAELupT4UAJQpAqRyLVN0z+bf6185OBX3AHTxCnucvf6DEdZ3/zXVaiRq7hLf7+RRQtQ==", "80507424-0372-444a-b565-06dd9803897a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6636c06f-952d-4756-93a6-735a41b17602", "AQAAAAEAACcQAAAAEHZyIhNjCvcxamIHlEjSMnQNeeztH9gFCW/ZdUpBRI0f7Z+U0V2Sk4boIldupN5+QA==", "94f6e9aa-0175-489a-84b6-f1337a8efd34" });
        }
    }
}
