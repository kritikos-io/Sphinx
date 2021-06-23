namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.DataProtection;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.UI.Services;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [Route("api/account")]
  public class AccountController : BaseController<AccountController>
  {
    private readonly IDataProtector dataProtector;
    private readonly UserManager<SphinxUser> userManager;
    private readonly RoleManager<SphinxRole> roleManager;
    private readonly RazorToStringRenderer razor;

    public AccountController(
      IDataProtectionProvider protectionProvider,
      RazorToStringRenderer razorRenderer,
      UserManager<SphinxUser> usersManager,
      RoleManager<SphinxRole> rolesManager,
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<AccountController> logger,
      IEmailSender sender,
      UserManager<SphinxUser> userManager)
      : base(dbContext, mapper, logger,userManager)
    {
      dataProtector = protectionProvider.CreateProtector(DataProtectionPurposes.UserManagement);
      userManager = usersManager;
      roleManager = rolesManager;
      razor = razorRenderer;
      this.sender = sender;
    }

    private IEmailSender sender { get; }

    [HttpGet("")]
    public ActionResult Foo()
    {
      sender.SendEmailAsync("akritikos@outlook.com", "Boo", "Blah");
      return Ok();
    }
  }
}
