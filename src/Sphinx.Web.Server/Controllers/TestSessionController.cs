namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Shared.CreateDto;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/testsession")]
  [ApiController]
  public class TestSessionController : BaseController<TestSessionController>
  {
    private readonly SphinxDbContext dbContext;

    public TestSessionController(ILogger<TestSessionController> logger, SphinxDbContext dbContext)
      : base(logger)
    {
      this.dbContext = dbContext;
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateTestSession(CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testSession = new TestSession();

      dbContext.TestSessions.Add(testSession);
      await dbContext.SaveChangesAsync(cancellationToken);

      var dto = new TestSessionRetrieveDto() { Id = testSession.Id };

      return CreatedAtAction(nameof(RetrieveTestSession), new { id = testSession.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestSessionRetrieveDto>> RetrieveTestSession(
      Guid id,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testSession = await dbContext.TestSessions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (testSession == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(TestSession), id);
        return NotFound();
      }

      var dto = new TestSessionRetrieveDto { Id = testSession.Id };

      return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTestSession(Guid id, CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testSession = await dbContext.TestSessions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (testSession == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(TestSession), id);
        return NotFound();
      }

      dbContext.Remove(testSession);
      await dbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
