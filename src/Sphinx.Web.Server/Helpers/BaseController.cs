namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using System.Threading.Tasks;

  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [ApiController]
  public abstract class BaseController<T> : ControllerBase
    where T : BaseController<T>
  {
    protected BaseController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<T> logger,
      UserManager<SphinxUser> userManager)
    {
      Logger = logger;
      DbContext = dbContext;
      Mapper = mapper;
      UserManager = userManager;
    }

    protected ILogger<T> Logger { get; }

    protected IPureMapper Mapper { get; }

    protected SphinxDbContext DbContext { get; }

    protected UserManager<SphinxUser> UserManager { get; }

    protected async Task<SphinxUser?> GetAuthenticatedUser()
      => User.Identity?.IsAuthenticated ?? false
        ? await UserManager.GetUserAsync(this.User)
        : null;
  }
}
