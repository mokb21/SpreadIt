using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpreadIt.IdSrv.Data.Migrations.IdentityServer.ConfigurationDb
{
    public partial class SeedConfigurationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApiResources",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Enabled", "LastAccessed", "Name", "NonEditable", "Updated" },
                values: new object[] { 1, new DateTime(2020, 4, 8, 6, 38, 53, 304, DateTimeKind.Utc).AddTicks(3669), null, "SpreadIt API", true, null, "spreadItAPI", false, null });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AbsoluteRefreshTokenLifetime", "AccessTokenLifetime", "AccessTokenType", "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AllowPlainTextPkce", "AllowRememberConsent", "AlwaysIncludeUserClaimsInIdToken", "AlwaysSendClientClaims", "AuthorizationCodeLifetime", "BackChannelLogoutSessionRequired", "BackChannelLogoutUri", "ClientClaimsPrefix", "ClientId", "ClientName", "ClientUri", "ConsentLifetime", "Created", "Description", "DeviceCodeLifetime", "EnableLocalLogin", "Enabled", "FrontChannelLogoutSessionRequired", "FrontChannelLogoutUri", "IdentityTokenLifetime", "IncludeJwtId", "LastAccessed", "LogoUri", "NonEditable", "PairWiseSubjectSalt", "ProtocolType", "RefreshTokenExpiration", "RefreshTokenUsage", "RequireClientSecret", "RequireConsent", "RequirePkce", "SlidingRefreshTokenLifetime", "UpdateAccessTokenClaimsOnRefresh", "Updated", "UserCodeType", "UserSsoLifetime" },
                values: new object[] { 1, 2592000, 3600, 0, false, false, false, true, false, false, 300, true, null, "client_", "apiClient", "Client to access api", null, null, new DateTime(2020, 4, 8, 6, 38, 53, 306, DateTimeKind.Utc).AddTicks(8411), null, 300, true, true, true, null, 300, false, null, null, false, null, "oidc", 1, 1, true, true, false, 1296000, false, null, null, null });

            migrationBuilder.InsertData(
                table: "IdentityResources",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Emphasize", "Enabled", "Name", "NonEditable", "Required", "ShowInDiscoveryDocument", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 4, 8, 6, 38, 53, 306, DateTimeKind.Utc).AddTicks(3419), null, "Your user identifier", false, true, "openid", false, true, true, null },
                    { 2, new DateTime(2020, 4, 8, 6, 38, 53, 306, DateTimeKind.Utc).AddTicks(4501), "Your user profile information (first name, last name, etc.)", "User profile", true, true, "profile", false, false, true, null }
                });

            migrationBuilder.InsertData(
                table: "ApiScopes",
                columns: new[] { "Id", "ApiResourceId", "Description", "DisplayName", "Emphasize", "Name", "Required", "ShowInDiscoveryDocument" },
                values: new object[] { 1, 1, null, "spreadItAPI", false, "spreadItAPI", false, true });

            migrationBuilder.InsertData(
                table: "ClientGrantTypes",
                columns: new[] { "Id", "ClientId", "GrantType" },
                values: new object[] { 1, 1, "client_credentials" });

            migrationBuilder.InsertData(
                table: "ClientScopes",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[] { 1, 1, "spreadItAPI" });

            migrationBuilder.InsertData(
                table: "ClientSecrets",
                columns: new[] { "Id", "ClientId", "Created", "Description", "Expiration", "Type", "Value" },
                values: new object[] { 1, 1, new DateTime(2020, 4, 8, 6, 38, 53, 308, DateTimeKind.Utc).AddTicks(768), null, null, "SharedSecret", "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=" });

            migrationBuilder.InsertData(
                table: "IdentityClaims",
                columns: new[] { "Id", "IdentityResourceId", "Type" },
                values: new object[,]
                {
                    { 1, 1, "sub" },
                    { 2, 2, "email" },
                    { 3, 2, "website" },
                    { 4, 2, "given_name" },
                    { 5, 2, "family_name" },
                    { 6, 2, "name" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApiScopes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientSecrets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IdentityClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApiResources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityResources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IdentityResources",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
