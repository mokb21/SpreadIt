using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.IdSrv.Data.Migrations.AspNetIdentity.AspNetIdentityDb
{
    public partial class SeedIdentityDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "859cb4b2-73a9-4f36-9e16-0a6029794862", "AliceSmith@email.com", true, false, null, "ALICESMITH@EMAIL.COM", "ALICE", "AQAAAAEAACcQAAAAELupT4UAJQpAqRyLVN0z+bf6185OBX3AHTxCnucvf6DEdZ3/zXVaiRq7hLf7+RRQtQ==", null, false, "80507424-0372-444a-b565-06dd9803897a", false, "alice" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2", 0, "6636c06f-952d-4756-93a6-735a41b17602", "BobSmith@email.com", true, false, null, "BOBSMITH@EMAIL.COM", "BOB", "AQAAAAEAACcQAAAAEHZyIhNjCvcxamIHlEjSMnQNeeztH9gFCW/ZdUpBRI0f7Z+U0V2Sk4boIldupN5+QA==", null, false, "94f6e9aa-0175-489a-84b6-f1337a8efd34", false, "bob" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "name", "Alice Smith", "1" },
                    { 2, "given_name", "Alice", "1" },
                    { 3, "family_name", "Smith", "1" },
                    { 4, "email", "AliceSmith@email.com", "1" },
                    { 5, "website", "http://alice.com", "1" },
                    { 11, "email_verified", "True", "1" },
                    { 13, "address", "{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", "1" },
                    { 15, "location", "somewhere", "1" },
                    { 6, "name", "Bob Smith", "2" },
                    { 7, "given_name", "Bob", "2" },
                    { 8, "family_name", "Smith", "2" },
                    { 9, "email", "BobSmith@email.com", "2" },
                    { 10, "website", "http://bob.com", "2" },
                    { 12, "email_verified", "True", "2" },
                    { 14, "address", "{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", "2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");
        }
    }
}
