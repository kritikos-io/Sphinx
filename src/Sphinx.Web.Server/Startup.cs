namespace Kritikos.Sphinx.Web.Server
{
  using System;
  using System.IdentityModel.Tokens.Jwt;
  using System.IO;
  using System.Linq;

  using IdentityServer4.Models;

  using Kritikos.Configuration.Persistence.Extensions;
  using Kritikos.Configuration.Persistence.Interceptors.SaveChanges;
  using Kritikos.Configuration.Persistence.Interceptors.Services;
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Web.CommonIdentity;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;

  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.DataProtection;
  using Microsoft.AspNetCore.Diagnostics.HealthChecks;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.UI.Services;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.OpenApi.Models;

  using Serilog;

  using Swashbuckle.AspNetCore.Filters;
  using Swashbuckle.AspNetCore.SwaggerUI;

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
      services.AddHttpContextAccessor();
      services.AddScoped<RazorToStringRenderer>();
      services.AddApplicationInsightsTelemetry();
      services.AddSingleton<IPureMapper>(_ => new PureMapper(MapProfile.DtoMapping));

      services.AddSingleton<IAuditorProvider<Guid>, AuditorProvider>();
      services.AddSingleton<AuditSaveChangesInterceptor<Guid>>();
      services.AddSingleton<TimestampSaveChangesInterceptor>();

      services.AddDbContextPool<SphinxDbContext>((serviceProvider, options) => options
        .UseNpgsql(
          Configuration.GetConnectionString("Sphinx"),
          pgsql => pgsql.EnableRetryOnFailure(3))
        .EnableCommonOptions(Environment)
        .AddInterceptors(
          serviceProvider.GetRequiredService<TimestampSaveChangesInterceptor>(),
          serviceProvider.GetRequiredService<AuditSaveChangesInterceptor<Guid>>()));

      services.AddDbContextPool<DataProtectionDbContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("MyKeysConnection")));

      services.AddDataProtection()
        .SetApplicationName($"{Environment.ApplicationName}-{Environment.EnvironmentName}")
        .PersistKeysToDbContext<DataProtectionDbContext>();

      services
        .AddHealthChecks()
        .AddDbContextCheck<SphinxDbContext>(nameof(SphinxDbContext))
        .AddDbContextCheck<DataProtectionDbContext>(nameof(DataProtectionDbContext));

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));
      if (string.IsNullOrWhiteSpace(Configuration["SendGrid:ApiKey"]))
      {
        services.AddSingleton<IEmailSender, DummyEmailSender>();
      }
      else
      {
        services.AddSingleton<IEmailSender, EmailSender>();
      }

      services.AddIdentity<SphinxUser, SphinxRole>(options =>
        {
          var isDevelopment = Environment.IsDevelopment();

          options.SignIn.RequireConfirmedAccount = true;
          options.SignIn.RequireConfirmedEmail = true;
          options.SignIn.RequireConfirmedPhoneNumber = false;

          options.User.RequireUniqueEmail = !isDevelopment;

          options.Password.RequireDigit = !isDevelopment;
          options.Password.RequireLowercase = !isDevelopment;
          options.Password.RequireNonAlphanumeric = !isDevelopment;
          options.Password.RequireUppercase = !isDevelopment;
        })
        .AddEntityFrameworkStores<SphinxDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

      services
        .AddIdentityServer()
        .AddApiAuthorization<SphinxUser, SphinxDbContext>(options =>
        {
          options.IdentityResources[new IdentityResources.OpenId().Name]
            .UserClaims.Add("role");
          options.IdentityResources[new IdentityResources.OpenId().Name]
            .UserClaims.Add(SphinxClaimHelpers.ClaimBaseName);

          options.ApiResources.Single()
            .UserClaims
            .Add("role");
          options.ApiResources.Single()
            .UserClaims
            .Add(SphinxClaimHelpers.ClaimBaseName);
        });
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove(SphinxClaimHelpers.ClaimBaseName);

      services.AddAuthentication()
        .AddIdentityServerJwt();

      services.AddSwaggerGen(swag =>
      {
        swag.SwaggerDoc(
          "v1",
          new OpenApiInfo { Title = "Sphinx API", Version = "v1", });

        swag.EnableAnnotations(true, true);
        swag.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        swag.OperationFilter<SecurityRequirementsOperationFilter>();

        swag.DescribeAllParametersInCamelCase();

        swag.IncludeXmlComments(Path.Combine(
          AppContext.BaseDirectory,
          $"{typeof(Startup).Assembly.GetName().Name}.xml"));
      });

      services.AddCorrelation();

      services.AddControllersWithViews();
      services.AddMvc();
      services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseSwagger();
      if (Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
        app.UseWebAssemblyDebugging();
        app.UseSwaggerUI(swag =>
        {
          swag.SwaggerEndpoint("/swagger/v1/swagger.json", "Sphinx API v1");
          swag.DocExpansion(DocExpansion.None);
          swag.EnableDeepLinking();
          swag.EnableFilter();
          swag.EnableValidator();
          swag.DisplayOperationId();
          swag.DisplayRequestDuration();
          swag.ShowExtensions();
        });
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseRouting();
      app.UseReDoc(c =>
      {
        c.RoutePrefix = "docs";
        c.DocumentTitle = "Psyche.Web.Host v1";
        c.SpecUrl("/swagger/v1/swagger.json");
        c.ExpandResponses("none");
        c.RequiredPropsFirst();
        c.SortPropsAlphabetically();
        c.HideDownloadButton();
        c.HideHostname();
      });

      app.UseHttpsRedirection();
      app.UseCorrelation();
      app.UseBlazorFrameworkFiles();
      app.UseStaticFiles();

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
        endpoints.MapHealthChecks(
          "/status",
          new HealthCheckOptions() { Predicate = r => true, AllowCachingResponses = false, });
        endpoints.MapControllers();
        endpoints.MapRazorPages();
        endpoints.MapFallbackToFile("index.html");
      });
    }
  }
}
