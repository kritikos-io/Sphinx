namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [ApiController]
  public abstract class BaseController<T> : ControllerBase
    where T : BaseController<T>
  {
    protected BaseController(ILogger<T> logger)
    {
      Logger = logger;
    }

    protected ILogger<T> Logger { get; }
  }
}
