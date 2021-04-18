namespace Kritikos.Sphinx.Web.Client.Helpers
{
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  public class CorrelationMessageHandler : DelegatingHandler
  {
    #region Overrides of DelegatingHandler
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      return base.Send(request, cancellationToken);
    }

    protected override Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      return base.SendAsync(request, cancellationToken);
    }

    #endregion
  }
}
