namespace Kritikos.Sphinx.Web.Server.Helpers.Log
{
  using Serilog.Core;
  using Serilog.Events;

  public class OperationIdEnricher : ILogEventEnricher
  {
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
      if (logEvent.Properties.TryGetValue("RequestId", out var requestId))
      {
        logEvent.AddPropertyIfAbsent(new LogEventProperty("operationId", requestId));
      }
    }
  }
}
