﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpreadIt.Repository.Models;

[assembly: HostingStartup(typeof(SpreadIt.IdSrv.Areas.Identity.IdentityHostingStartup))]
namespace SpreadIt.IdSrv.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<SpreadItIdSrvDbContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("SpreadItIdSrvDbContextConnection")));

            //    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<SpreadItIdSrvDbContext>();
            //});

            builder.ConfigureServices((context, services) =>
            {

            });
        }
    }
}