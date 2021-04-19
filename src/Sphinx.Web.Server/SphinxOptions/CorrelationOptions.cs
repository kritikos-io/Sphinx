namespace Kritikos.Sphinx.Web.Server.SphinxOptions
{
  using Microsoft.Extensions.Logging;

  public class CorrelationOptions
  {
    public const string DefaultHeader = "X-Correlation-ID";

    /// <summary>
    /// Name of the header field that will store the correlation id.
    /// </summary>
    public string Header { get; set; } = DefaultHeader;

    /// <summary>
    /// Whether the correlation id should be included in response headers.
    /// </summary>
    public bool IncludeInResponses { get; set; } = true;

    /// <summary>
    /// Log level that should be used to report requests with missing correlation id.
    /// </summary>
    public LogLevel MissingCorrelationLogLevel { get; set; } = LogLevel.Debug;
  }
}
