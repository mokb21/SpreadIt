using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.Repository.Migrations.AspNetIdentity
{
    public partial class CustomPropertiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31d0f17b-d525-4410-9c8d-c370f893c46b", "AQAAAAEAACcQAAAAEAxY7kKbHG3NTL/GhsAiSbduNZg7nzjp0LNAYmkRm2msTEPx6MntBM0IxTa9VPDEog==", "2ef8c743-b182-494e-9742-27b39e301338" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0654fbc5-76df-4344-865b-18ba3598bcc6", "AQAAAAEAACcQAAAAEOCuEdkI3G8wA4kJo2aLTJPyFvZ+0SuQocaNlV6/upNFtza/yFcpkWB/UgLtkmo+mQ==", "0c458663-d7e5-42a7-bbf1-913767c0c1b6" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
