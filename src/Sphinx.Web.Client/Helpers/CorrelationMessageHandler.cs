namespace Kritikos.Sphinx.Web.Client.Helpers
{
  using System;
  using System.Linq;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  public class CorrelationMessageHandler : DelegatingHandler
  {
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    #region Overrides of DelegatingHandler
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      request.Headers.TryGetValues(CorrelationIdHeaderName, out var values);
      if (!values?.Any() ?? false)
      {
        request.Headers.Add(CorrelationIdHeaderName, Guid.NewGuid().ToString("D"));
      }

      return base.Send(request, cancellationToken);
    }

    #endregion
  }
}
