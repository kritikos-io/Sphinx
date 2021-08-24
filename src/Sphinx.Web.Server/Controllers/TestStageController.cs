namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Extensions.Linq;
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;
  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.Criteria;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [ApiController, Route("api/[controller]")]
  public class TestStageController : BaseController<DatasetController>
  {
    public TestStageController(
      SphinxDbContext dbContext,
      UserManager<SphinxUser> userManager,
      IPureMapper mapper,
      ILogger<DatasetController> logger)
      : base(dbContext, mapper, logger, userManager)
    {
    }

    [HttpPost]
    public async Task<ActionResult<TestStageRetrieveDto>> CreateTestStage(
      TestStageCreateDto model,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testStage = Mapper.Map<TestStageCreateDto, TestStage>(model);

      DbContext.TestStages.Add(testStage);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<TestStage, TestStageRetrieveDto>(testStage);

      return CreatedAtAction(nameof(RetrieveTestStage), new { id = testStage.Id }, dto);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<TestStageRetrieveDto>>> RetrieveAll(
      PaginationCriteria pagination,
      CancellationToken cancellationToken = default)
    {
      var query = DbContext.TestStages;

      var total = await query.CountAsync(cancellationToken);

      var testStages = await query
        .OrderBy(x => x.Id)
        .Slice(pagination.Page, pagination.ItemsPerPage)
        .Project<TestStage, TestStageRetrieveDto>(Mapper)
        .ToListAsync(cancellationToken);

      var result = testStages.Paginate(pagination, total);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestStageRetrieveDto>> RetrieveTestStage(
      long id,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testStage = await DbContext.TestStages
        .Where(x => x.Id == id)
        .Project<TestStage, TestStageRetrieveDto>(Mapper)
        .SingleOrDefaultAsync(cancellationToken);

      if (testStage == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      return Ok(testStage);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTestStage(long id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var testStage = await DbContext.TestStages.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (testStage == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      DbContext.TestStages.Remove(testStage);
      await DbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
