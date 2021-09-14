namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Security.Claims;
  using System.Threading.Tasks;

  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Web.CommonIdentity;
  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [Microsoft.AspNetCore.Components.Route("api/admin")]
  public class AdminController : BaseController<AdminController>
  {
    public AdminController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<AdminController> logger,
      UserManager<SphinxUser> userManager,
      RoleManager<SphinxRole> roleManager)
      : base(dbContext, mapper, logger, userManager)
    {
      RoleManager = roleManager;
    }

    RoleManager<SphinxRole> RoleManager { get; }

    [HttpPost("addToRole")]
    public async Task<ActionResult> AddUserToRole(string email, string roleName)
    {
      var user = await UserManager.FindByEmailAsync(email);
      if (user == null)
      {
        return NotFound("no such user");
      }

      var role = await RoleManager.FindByNameAsync(roleName);
      if (role == null)
      {
        return NotFound("no such role");
      }

      if (await UserManager.IsInRoleAsync(user, roleName))
      {
        return StatusCode((int)HttpStatusCode.NotModified);
      }

      var result = await UserManager.AddToRoleAsync(user, roleName);
      return Ok(result);
    }

    [HttpPost("role/{name}/claims")]
    public async Task<ActionResult<Claim>> AddRoleClaim(string name, string claimName)
    {
      var role = await RoleManager.FindByNameAsync(name);
      var claims = await RoleManager.GetClaimsAsync(role);

      if (claims.Any(x => x.Value == claimName))
      {
        return StatusCode((int)HttpStatusCode.NotModified);
      }

      var claim = new Claim(SphinxClaimHelpers.ClaimBaseName, claimName);
      await RoleManager.AddClaimAsync(role, claim);
      return Ok(claim);
    }

    [HttpGet("role/{name}/claims")]
    public async Task<ActionResult<List<Claim>>> GetRoleClaims(string name)
    {
      var role = await RoleManager.FindByNameAsync(name);
      var claims = await RoleManager.GetClaimsAsync(role);
      return Ok(claims);
    }

    [HttpGet("role/{name}/users")]
    public async Task<ActionResult<List<string>>> GetUsersInRole(string name)
    {
      var users = await UserManager.GetUsersInRoleAsync(name);
      return Ok(users.Select(x => x.Email).ToList());
    }
  }
}
