namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.Extensions.Logging;

  public abstract class BasePageModel<T> : PageModel
    where T : BasePageModel<T>
  {
    protected BasePageModel(ILogger<T> logger)
    {
      Logger = logger;
    }

    protected ILogger<T> Logger { get; }
  }
}
