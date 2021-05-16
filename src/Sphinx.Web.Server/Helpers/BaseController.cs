namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [ApiController]
  public abstract class BaseController<T> : ControllerBase
    where T : BaseController<T>
  {
    protected BaseController(SphinxDbContext dbContext, IPureMapper mapper, ILogger<T> logger)
    {
      Logger = logger;
      DbContext = dbContext;
      Mapper = mapper;
    }

    protected ILogger<T> Logger { get; }

    protected IPureMapper Mapper { get; }

    protected SphinxDbContext DbContext { get; }
  }
}
