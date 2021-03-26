namespace Kritikos.Sphinx.Web.Server
{
  using Kritikos.Configuration.Persistence.Extensions;
  using Kritikos.Configuration.Persistence.Services;
  using Kritikos.Kerberos.Persistence;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  using Serilog;

  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApplicationInsightsTelemetry();

      services.AddDbContextPool<SphinxDbContext>(
        options => options.UseNpgsql(
            Configuration.GetConnectionString("Sphinx"),
            pgsql => pgsql.EnableRetryOnFailure(3))
          .EnableCommonOptions(Environment));
      services.AddDbContextPool<KerberosDbContext>(
        options => options.UseNpgsql(
            Configuration.GetConnectionString("Kerberos"),
            pgsql => pgsql.EnableRetryOnFailure(3))
          .EnableCommonOptions(Environment));

      if (Environment.IsDevelopment())
      {
        services.AddHostedService<MigrationService<SphinxDbContext>>();
        services.AddHostedService<MigrationService<KerberosDbContext>>();
      }

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddDefaultIdentity<SphinxUser>(options =>
        {
          var isDevelopment = Environment.IsDevelopment();

          options.SignIn.RequireConfirmedAccount = !isDevelopment;
          options.SignIn.RequireConfirmedEmail = !isDevelopment;

          options.User.RequireUniqueEmail = !isDevelopment;

          options.Password.RequireDigit = !isDevelopment;
          options.Password.RequireLowercase = !isDevelopment;
          options.Password.RequireNonAlphanumeric = !isDevelopment;
          options.Password.RequireUppercase = !isDevelopment;
        })
        .AddEntityFrameworkStores<SphinxDbContext>();

      services.AddIdentityServer()
        .AddApiAuthorization<SphinxUser, SphinxDbContext>();

      services.AddAuthentication()
        .AddIdentityServerJwt();

      services.AddControllersWithViews();
      services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app)
    {
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
        app.UseWebAssemblyDebugging();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseBlazorFrameworkFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseSerilogIngestion(obj =>
      {
        obj.ClientLevelSwitch = Program.LevelSwitch;
        obj.OriginPropertyName = "InstanceId";
      });
      app.UseSerilogRequestLogging();

      app.UseIdentityServer();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
      });
    }
  }
}
