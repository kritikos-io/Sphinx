namespace Kritikos.Sphinx.Web.Server.Middleware
{
  using System;
  using System.Globalization;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Server.SphinxOptions;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using Serilog.Context;

  public class CorrelationMiddleware : IMiddleware
  {
    private const string IncomingRequestWithCorrelation = "Received request with correlation header {Header}";

    private const string IncomingRequestWithoutCorrelation = "Incoming request is missing correlation header, generated {Header}";

    private readonly CorrelationOptions options;

    private readonly ILogger<CorrelationMiddleware> logger;

    public CorrelationMiddleware(IOptions<CorrelationOptions> correlationOptions, ILogger<CorrelationMiddleware> correlationLogger)
    {
      options = correlationOptions?.Value ?? throw new ArgumentNullException(nameof(correlationOptions));
      logger = correlationLogger;
    }

    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
      if (next == null)
      {
        throw new ArgumentNullException(nameof(next));
      }

      if (context.Request.Headers.TryGetValue(options.Header, out var value))
      {
        context.TraceIdentifier = value;
        logger.LogTrace(IncomingRequestWithCorrelation, context.TraceIdentifier);
      }
      else
      {
        context.TraceIdentifier = Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture);
        logger.Log(
          options.MissingCorrelationLogLevel,
          IncomingRequestWithoutCorrelation,
          context.TraceIdentifier);
      }

      if (options.IncludeInResponses)
      {
        context.Response.Headers.Add(options.Header, new[] { context.TraceIdentifier });
      }

      using var ctx = LogContext.PushProperty("CorrelationId", context.TraceIdentifier);

      return next(context);
    }
  }
}
