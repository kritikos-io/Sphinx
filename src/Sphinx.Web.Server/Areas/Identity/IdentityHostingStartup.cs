using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Kritikos.Sphinx.Web.Server.Areas.Identity.IdentityHostingStartup))]

namespace Kritikos.Sphinx.Web.Server.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
  {
    public void Configure(IWebHostBuilder builder) => builder.ConfigureServices((context, services) => { });
  }
}
