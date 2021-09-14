namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Models.RetrieveDto;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/dataset")]
  public class DatasetController : BaseController<DatasetController>
  {
    public DatasetController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<DatasetController> logger,
      UserManager<SphinxUser> userManager)
      : base(dbContext, mapper, logger, userManager)
    {
    }

    [HttpGet("grouped")]
    public async Task<ActionResult<List<StimuliGroupWithDatasetDto>>> RetrieveDatasetGroups(CancellationToken token = default!)
    {
      var grouped = await DbContext.StimulusGroups
        .Project<StimuliGroup, StimuliGroupWithDatasetDto>(Mapper)
        .ToListAsync(token);

      return Ok(grouped);
    }
  }
}
