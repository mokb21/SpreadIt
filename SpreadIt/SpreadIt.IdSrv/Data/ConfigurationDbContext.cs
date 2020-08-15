using IdentityModel;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadIt.IdSrv.Data
{
    public class ConfigurationDbContext : IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext<ConfigurationDbContext>
    {
        private readonly ConfigurationStoreOptions _storeOptions;

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
            _storeOptions = storeOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(_storeOptions);
            modelBuilder.ConfigureResourcesContext(_storeOptions);

            base.OnModelCreating(modelBuilder);

            ClientSeed(modelBuilder);
        }

        private void ClientSeed(ModelBuilder builder)
        {
            builder.Entity<ApiResource>()
                .HasData(
                    new ApiResource
                    {
                        Id = 1,
                        Name = "spreadItAPI",
                        DisplayName = "SpreadIt API"
                    }
                );

            builder.Entity<ApiScope>()
                .HasData(
                    new ApiScope
                    {
                        Id = 1,
                        Name = "spreadItAPI",
                        DisplayName = "spreadItAPI",
                        Description = null,
                        Required = false,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        ApiResourceId = 1
                    }
                );

            builder.Entity<IdentityResource>().HasData
                (
                    new IdentityResource()
                    {
                        Id = 1,
                        Enabled = true,
                        Name = "openid",
                        DisplayName = "Your user identifier",
                        Description = null,
                        Required = true,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    },
                    new IdentityResource()
                    {
                        Id = 2,
                        Enabled = true,
                        Name = "profile",
                        DisplayName = "User profile",
                        Description = "Your user profile information (first name, last name, etc.)",
                        Required = false,
                        Emphasize = true,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    });

            builder.Entity<IdentityClaim>()
                .HasData(
                    new IdentityClaim
                    {
                        Id = 1,
                        IdentityResourceId = 1,
                        Type = "sub"
                    },
                    new IdentityClaim
                    {
                        Id = 2,
                        IdentityResourceId = 2,
                        Type = "email"
                    },
                    new IdentityClaim
                    {
                        Id = 3,
                        IdentityResourceId = 2,
                        Type = "website"
                    },
                    new IdentityClaim
                    {
                        Id = 4,
                        IdentityResourceId = 2,
                        Type = "given_name"
                    },
                    new IdentityClaim
                    {
                        Id = 5,
                        IdentityResourceId = 2,
                        Type = "family_name"
                    },
                    new IdentityClaim
                    {
                        Id = 6,
                        IdentityResourceId = 2,
                        Type = "name"
                    });

            builder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 1,
                        Enabled = true,
                        ClientId = "apiClient",
                        ProtocolType = "oidc",
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = "Client to access api",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    });

            builder.Entity<ClientGrantType>()
                .HasData(
                    new ClientGrantType
                    {
                        Id = 1,
                        GrantType = "client_credentials",
                        ClientId = 1
                    });

            builder.Entity<ClientScope>()
                .HasData(
                    new ClientScope
                    {
                        Id = 1,
                        Scope = "spreadItAPI",
                        ClientId = 1,
                    });

            builder.Entity<ClientSecret>()
                .HasData(
                        new ClientSecret
                        {
                            Id = 1,
                            Value = "secret".ToSha256(),
                            Type = "SharedSecret",
                            ClientId = 1
                        });


            builder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = 2,
                        Enabled = true,
                        ClientId = "mobileClient",
                        ProtocolType = "oidc",
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = "Client to mobile app",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = true,
                        AllowOfflineAccess = true,
                    });

            builder.Entity<ClientGrantType>()
                .HasData(
                    new ClientGrantType
                    {
                        Id = 2,
                        GrantType = "client_credentials",
                        ClientId = 2
                    });

            builder.Entity<ClientScope>()
                .HasData(
                    new ClientScope
                    {
                        Id = 2,
                        Scope = "spreadItAPI",
                        ClientId = 2,
                    });

            builder.Entity<ClientSecret>()
                .HasData(
                        new ClientSecret
                        {
                            Id = 2,
                            Value = "secret".ToSha256(),
                            Type = "SharedSecret",
                            ClientId = 2
                        });


            //builder.Entity<ClientPostLogoutRedirectUri>()
            //    .HasData(
            //    new ClientPostLogoutRedirectUri
            //    {
            //        Id = 1,
            //        PostLogoutRedirectUri = "http://localhost:5002/signout-callback-oidc",
            //        ClientId = 3
            //    },
            //    new ClientPostLogoutRedirectUri
            //    {
            //        Id = 2,
            //        PostLogoutRedirectUri = "http://localhost:5003/index.html",
            //        ClientId = 4
            //    });

            //builder.Entity<ClientRedirectUri>()
            //    .HasData(
            //    new ClientRedirectUri
            //    {
            //        Id = 1,
            //        RedirectUri = "http://localhost:5002/signin-oidc",
            //        ClientId = 3
            //    },
            //    new ClientRedirectUri
            //    {
            //        Id = 2,
            //        RedirectUri = "http://localhost:5003/callback.html",
            //        ClientId = 4
            //    });

            //builder.Entity<ClientCorsOrigin>()
            //    .HasData(
            //    new ClientCorsOrigin
            //    {
            //        Id = 1,
            //        Origin = "http://localhost:5003",
            //        ClientId = 4
            //    });
        }
    }
}
