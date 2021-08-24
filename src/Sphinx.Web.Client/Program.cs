namespace Kritikos.Sphinx.Web.Client
{
  using System;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Client.Helpers;
  using Kritikos.Sphinx.Web.Shared.API;

  using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
  using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
  using Microsoft.Extensions.DependencyInjection;

  using Refit;

  using Serilog;
  using Serilog.Core;
  using Serilog.Events;

  using Syncfusion.Blazor;

  public static class Program
  {
    private static readonly LoggingLevelSwitch LevelSwitch = new(LogEventLevel.Information);

    public static async Task Main(string[] args)
    {
      Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
        @"NDkxODMwQDMxMzkyZTMyMmUzMGVydmRWWkdNQ24rbXlzdUZVSDYzSVNtbVU1R2pHLzMyZkpXWitpR0RHQU09;NDkxODMxQDMxMzkyZTMyMmUzMGJ3eWI1ZXUva1VBWWZvZTQ3OW5MMlUvOVgyTGF0OEJ6eWhDR0hzVHd1UW89;NDkxODMyQDMxMzkyZTMyMmUzMG1Obk9ha3ptTlhER3JpaW5ieWRSenZyR2pkNldUUEhDVU45ZkFlMGZZWVU9;NDkxODMzQDMxMzkyZTMyMmUzMEtOQkRObTVSR3dYYlc4Z3RQU1pVQmw0dVlRbUxkL0ZtMnAzODJXZEhQaDg9;NDkxODM0QDMxMzkyZTMyMmUzME0yTkM5SHFyU1cwMHdhSlkxM1ZNR0lZZWRXSnV5SVZCZG1VeUZuaGYwVGc9;NDkxODM1QDMxMzkyZTMyMmUzMGxjeEQwRmRjU3czR0pIYkI5eEhTRVJ1eEV1am5oeVJlM2dkZUN0TE1Qblk9;NDkxODM2QDMxMzkyZTMyMmUzMFI2UGZ1RWlwTGZ3Mlo4Zzl2cCtvRFk0ZUlFbEx1aldLY3pCSENjSW96Rkk9;NDkxODM3QDMxMzkyZTMyMmUzMEVCeFFkbFBZRG5IYmx3cHdnQ2pNUnE0cy9YcDMxNnlXR281cjl3VzZjdnM9;NDkxODM4QDMxMzkyZTMyMmUzMFpGS09MbFZaMUFvOWhtM0UwOGhlczVTemVzUDZFa0RPb0FRcEg0bnZPc009;NDkxODM5QDMxMzkyZTMyMmUzMGFiZ0pDQVZTQmNBd3l5L1RGVThnWmxZWVhPc1MvYjNjTlo0dkg0M29tRHM9");
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      Log.Logger = CreateLoggerBuilder($"{builder.HostEnvironment.BaseAddress}ingest").CreateLogger();

      builder.RootComponents.Add<App>("#app");

      builder.Services.AddRefitClient<ISphinxApi>()
        .ConfigureHttpClient(c =>
        {
          c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        })
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
        .AddHttpMessageHandler<CorrelationMessageHandler>();
      builder.Services.AddSingleton<CorrelationMessageHandler>();

      builder.Services.AddSyncfusionBlazor();

      builder.Services.AddApiAuthorization();
      builder.Services.AddLogging(configure => configure.AddSerilog());

      var host = builder.Build();

      await host.RunAsync();
    }

    private static LoggerConfiguration CreateLoggerBuilder(string endpoint)
      => new LoggerConfiguration()
        .MinimumLevel.ControlledBy(LevelSwitch)
        .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
        .WriteTo.BrowserHttp(
          controlLevelSwitch: LevelSwitch,
          endpointUrl: endpoint)
        .WriteTo.BrowserConsole();
  }
}
