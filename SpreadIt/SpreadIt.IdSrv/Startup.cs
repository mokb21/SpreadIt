// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using IdentityServer4.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using SpreadIt.Repository.Models;

namespace SpreadIt.IdSrv
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddMvc();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<SpreadItContext>(options =>
                options.UseSqlServer(Constants.SpreadItConstants.ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
            );

            services.AddDbContext<Data.ConfigurationDbContext>(options =>
            options.UseSqlServer(Constants.SpreadItConstants.ConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<SpreadItContext>()
            .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.UserInteraction.LoginUrl = "/Account/Login";
                options.UserInteraction.LogoutUrl = "/Account/Logout";
                options.Authentication = new AuthenticationOptions()
                {
                    CookieLifetime = TimeSpan.FromHours(10), // ID server cookie timeout set to 10 hours
                    CookieSlidingExpiration = true
                };
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                           b.UseSqlServer(Constants.SpreadItConstants.ConnectionString,
                               sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                            b.UseSqlServer(Constants.SpreadItConstants.ConnectionString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));

                options.EnableTokenCleanup = true;
            })
            .AddAspNetIdentity<ApplicationUser>();

            //var builder = services.AddIdentityServer()
            //    .AddInMemoryIdentityResources(Config.Ids)
            //    .AddInMemoryApiResources(Config.Apis)
            //    .AddInMemoryClients(Config.Clients)
            //    .AddTestUsers(Config.Users);

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
