using System;
using Kritikos.Sphinx.Data.Persistence;
using Kritikos.Sphinx.Data.Persistence.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Kritikos.Sphinx.Web.Server.Areas.Identity.IdentityHostingStartup))]
namespace Kritikos.Sphinx.Web.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}