namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Linq;
  using System.Security.Cryptography.X509Certificates;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Extensions.Linq;
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;
  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.Criteria;
  using Kritikos.Sphinx.Web.Shared.Enums;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/insignificantstimuli")]
  [ApiController]
  public class InsignificantStimuliController : BaseController<InsignificantStimuliController>
  {
    public InsignificantStimuliController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<InsignificantStimuliController> logger)
      : base(dbContext, mapper, logger)
    {
    }

    [HttpPost("")]
    public async Task<ActionResult<InsignificantStimulusRetrieveDto>> CreateInsignificantStimulus(
      InsignificantStimulusCreateDto model,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      if (model.Type is StimulusType.Significant)
      {
        return BadRequest("The type of the stimuli cannot be significant");
      }

      var datasets = await DbContext.DataSets.Select(x => x.Id).ToListAsync(cancellationToken);
      var dataset = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), model.DataSetId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }
      var stimulus = Mapper.Map<InsignificantStimulusCreateDto, InsignificantStimulus>(model);
      stimulus.DataSet = dataset;

      DbContext.InsignificantStimuli.Add(stimulus);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<InsignificantStimulus, InsignificantStimulusRetrieveDto>(stimulus);

      return CreatedAtAction(nameof(RetrieveInsignificantStimulus), new { id = stimulus.Id }, dto);
    }

    [HttpPost("fetch")]
    public async Task<ActionResult<PagedResult<InsignificantStimulusRetrieveDto>>> RetrieveAll(
      PaginationCriteria pagination,
      CancellationToken cancellationToken = default)
    {
      var query = DbContext.InsignificantStimuli;

      var total = await query.CountAsync(cancellationToken);

      var insignificantstimuli = await query
        .OrderBy(x => x.Id)
        .Slice(pagination.Page, pagination.ItemsPerPage)
        .Include(x => x.DataSet)
        .Project<InsignificantStimulus, InsignificantStimulusRetrieveDto>(Mapper)
        .ToListAsync(cancellationToken);

      var result = insignificantstimuli.Paginate(pagination, total);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InsignificantStimulusRetrieveDto>> RetrieveInsignificantStimulus(
      long id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await DbContext.InsignificantStimuli
        .Include(x => x.DataSet)
        .Where(x => x.Id == id)
        .Project<InsignificantStimulus, InsignificantStimulusRetrieveDto>(Mapper)
        .SingleOrDefaultAsync(cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(Stimulus), id);
        return NotFound();
      }

      return Ok(stimulus);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<InsignificantStimulusRetrieveDto>> UpdateInsignificantStimulus(
      long id, InsignificantStimulusUpdateDto model, CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      if (model.Type is StimulusType.Significant)
      {
        return BadRequest("You cannot update the Type of the Stimulus from Insignificant to Significant");
      }

      var stimulusToBeUpdated = await DbContext.InsignificantStimuli
        .Include(x => x.DataSet)
        .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulusToBeUpdated == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(InsignificantStimulus), id);
        return NotFound();
      }

      if (model.DataSetId != stimulusToBeUpdated.DataSet.Id)
      {
        var newDataSet = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);
        if (newDataSet == null)
        {
          return NotFound("The dataset you want to move the stimulus to does not exist");
        }

        stimulusToBeUpdated.DataSet = newDataSet;
      }

      Mapper.Map(model, stimulusToBeUpdated);

      DbContext.InsignificantStimuli.Update(stimulusToBeUpdated);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<InsignificantStimulus, InsignificantStimulusRetrieveDto>(stimulusToBeUpdated);

      return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInsignificantStimulus(long id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await DbContext.InsignificantStimuli.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(InsignificantStimulus), id);
        return NotFound();
      }

      DbContext.InsignificantStimuli.Remove(stimulus);
      await DbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
