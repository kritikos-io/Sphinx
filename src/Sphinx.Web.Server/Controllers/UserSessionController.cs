namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/usersession")]
  [ApiController]
  public class UserSessionController : BaseController<UserSessionController>
  {
    public UserSessionController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<UserSessionController> logger,
      UserManager<SphinxUser> userManager)
      : base(dbContext, mapper, logger,userManager)
    {
    }

    [HttpPost("")]
    public async Task<ActionResult<UserSessionRetrieveDto>> CreateUserSession(
      UserSessionCreateDto model,
      CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var user = await DbContext.Users.SingleOrDefaultAsync(x => x.Id == model.UserId, cancellationToken);

      if (user == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(User), model.UserId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }

      var testSession = await DbContext.TestSessions.SingleOrDefaultAsync(x => x.Id == model.TestSessionId, cancellationToken);

      var userSession = new UserSession
      {
        TestSession = testSession,
        User = user,
      };

      DbContext.UserSessions.Add(userSession);

      await DbContext.SaveChangesAsync();

      var dto = Mapper.Map<UserSession, UserSessionRetrieveDto>(userSession);
      return CreatedAtAction(nameof(CreateUserSession), dto);
    }

  }
}
