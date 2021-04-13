namespace Kritikos.Sphinx.Web.Server
{
  using System;
  using System.IO;
  using System.Security.Cryptography;

  using HealthChecks.UI.Client;

  using Kritikos.Configuration.Persistence.Extensions;
  using Kritikos.Configuration.Persistence.HealthCheck.DependencyInjection;
  using Kritikos.Configuration.Persistence.Services;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.DataProtection;
  using Microsoft.AspNetCore.Diagnostics.HealthChecks;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.IdentityModel.Tokens;
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
      services.AddApplicationInsightsTelemetry();

      services.AddDbContextPool<SphinxDbContext>(
        options => options.UseNpgsql(
            Configuration.GetConnectionString("Sphinx"),
            pgsql => pgsql.EnableRetryOnFailure(3))
          .EnableCommonOptions(Environment));

      services.AddDbContext<DataProtectionDbContext>(options =>
        options.UseNpgsql(
          Configuration.GetConnectionString("MyKeysConnection")));

      services.AddDataProtection()
        .SetApplicationName($"{Environment.ApplicationName}-{Environment.EnvironmentName}")
        .PersistKeysToDbContext<DataProtectionDbContext>();

      services.AddHealthChecksUI(setup =>
        {
          setup.SetHeaderText("Sphinx - Health Status");
          setup.AddHealthCheckEndpoint("self", "status");
          setup.SetEvaluationTimeInSeconds(60);
          setup.MaximumHistoryEntriesPerEndpoint(200);
        })
        .AddInMemoryStorage();

      services
        .AddHealthChecks()
        .AddDbContext<SphinxDbContext>()
        .AddSendGrid(Configuration["SendGrid:ApiKey"], name: "SendGrid")
        .AddAzureBlobStorage(Configuration.GetConnectionString("SphinxStorageAccount"), name: "Blob Storage")
        .AddAzureQueueStorage(Configuration.GetConnectionString("SphinxStorageAccount"), name: "Queue Storage")
        .AddSeqPublisher(seq =>
        {
          seq.ApiKey = Configuration["Seq:ApiKey"];
          seq.Endpoint = Configuration["Seq:Uri"];
        });

      services.AddHostedService<MigrationService<SphinxDbContext>>();
      services.AddHostedService<MigrationService<DataProtectionDbContext>>();

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

      var identity = services.AddIdentityServer();

      var pem = Configuration["Identity:Key"];

      if (string.IsNullOrWhiteSpace(pem))
      {
        identity.AddApiAuthorization<SphinxUser, SphinxDbContext>();
      }
      else
      {
        var key = ECDsa.Create();
        key.ImportECPrivateKey(Convert.FromBase64String(pem), out _);
        var credentials = new SigningCredentials(new ECDsaSecurityKey(key), "ES512");

        identity.AddApiAuthorization<SphinxUser, SphinxDbContext>(options =>
        {
          options.SigningCredential = credentials;
        });
      }

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
          new HealthCheckOptions()
          {
            Predicate = r => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            AllowCachingResponses = false,
          });
        endpoints.MapHealthChecksUI(setup =>
        {
          setup.UIPath = "/health";
          setup.AddCustomStylesheet("health.css");
        });
        endpoints.MapControllers();
        endpoints.MapRazorPages();
        endpoints.MapFallbackToFile("index.html");
      });
    }
  }
}
