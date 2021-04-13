namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.DataProtection;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [Microsoft.AspNetCore.Components.Route("api/account")]
  public class AccountController : BaseController<AccountController>
  {
    private readonly IDataProtector dataProtector;
    private readonly UserManager<SphinxUser> userManager;
    private readonly RoleManager<SphinxRole> roleManager;

    public AccountController(
      IDataProtectionProvider protectionProvider,
      UserManager<SphinxUser> usersManager,
      RoleManager<SphinxRole> rolesManager,
      ILogger<AccountController> logger)
      : base(logger)
    {
      dataProtector = protectionProvider.CreateProtector(DataProtectionPurposes.UserManagement);
      userManager = usersManager;
      roleManager = rolesManager;
    }

    public async Task<ActionResult> Register(CancellationToken cancellationToken)
    {
      var foo = new SphinxUser { Email = dataProtector.Protect("foobar@email.com"), };

      await userManager.CreateAsync(foo, "secretpassword");

      return Ok();
    }
  }
}
