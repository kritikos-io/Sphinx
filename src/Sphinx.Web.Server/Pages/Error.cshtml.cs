namespace Kritikos.Sphinx.Web.Server.Pages
{
  using System.Diagnostics;

  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.Extensions.Logging;

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  [IgnoreAntiforgeryToken]
  public class ErrorModel : BasePageModel<ErrorModel>
  {
    public ErrorModel(ILogger<ErrorModel> logger)
      : base(logger)
    {
    }

    public string RequestId { get; set; } = string.Empty;

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public void OnGet()
    {
      RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
      Logger.LogError(LogTemplates.Razor.ErrorHandlingRequest, RequestId);
    }
  }
}
