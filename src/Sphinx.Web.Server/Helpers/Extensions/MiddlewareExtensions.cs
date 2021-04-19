namespace Kritikos.Sphinx.Web.Server.Helpers.Extensions
{
  using System;

  using Kritikos.Sphinx.Web.Server.Middleware;
  using Kritikos.Sphinx.Web.Server.SphinxOptions;

  using Microsoft.AspNetCore.Builder;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Options;

  public static class MiddlewareExtensions
  {
    /// <summary>
    /// Enables correlation ids for requests.
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/> to act on.</param>
    /// <returns><see cref="IApplicationBuilder"/> to allow chained calls.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="app"/> is null.</exception>
    public static IApplicationBuilder UseCorrelation(this IApplicationBuilder app) =>
      app == null
        ? throw new ArgumentNullException(nameof(app))
        : app.UseMiddleware<CorrelationMiddleware>();

    /// <summary>
    /// Injects correlation middleware with default options.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to act on.</param>
    /// <returns><paramref name="services"/> to allow chained calls.</returns>
    public static IServiceCollection AddCorrelation(this IServiceCollection services)
      => services.AddCorrelation(new CorrelationOptions());

    /// <summary>
    /// Injects correlation middleware.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to act on.</param>
    /// <param name="options">Custom <see cref="CorrelationOptions"/>.</param>
    /// <returns><paramref name="services"/> to allow chained calls.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="options"/> or <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddCorrelation(this IServiceCollection services, CorrelationOptions options)
    {
      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      services?.AddSingleton(Options.Create(options));
      return services?.AddSingleton<CorrelationMiddleware>() ?? throw new ArgumentNullException(nameof(services));
    }
  }
}
