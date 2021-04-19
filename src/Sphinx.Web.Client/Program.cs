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

  public static class Program
  {
    private static readonly LoggingLevelSwitch LevelSwitch = new(LogEventLevel.Information);

    public static async Task Main(string[] args)
    {
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
