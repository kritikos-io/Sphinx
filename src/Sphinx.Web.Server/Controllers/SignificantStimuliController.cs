namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Extensions.Linq;
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;
  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.Criteria;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/significantstimuli")]
  [ApiController]
  public class SignificantStimuliController : BaseController<SignificantStimuliController>
  {
    public SignificantStimuliController(
      SphinxDbContext dbContext,
      IPureMapper mapper,
      ILogger<SignificantStimuliController> logger,
      UserManager<SphinxUser> userManager)
      : base(dbContext, mapper, logger, userManager)
    {
    }

    [HttpPost("/primary")]
    public async Task<ActionResult<SignificantStimulusRetrieveDto>> CreatePrimaryStimulus(
      PrimarySignificantStimulusCreateDto model,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), model.DataSetId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }

      var stimulus = Mapper.Map<PrimarySignificantStimulusCreateDto, SignificantStimulus>(model);
      stimulus.DataSet = dataset;

      DbContext.SignificantStimuli.Add(stimulus);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<SignificantStimulus, SignificantStimulusRetrieveDto>(stimulus);
      return CreatedAtAction(nameof(RetrieveSignificantStimulus), new { id = stimulus.Id }, dto);
    }

    [HttpPost("/secondary")]
    public async Task<ActionResult<SignificantStimulusRetrieveDto>> CreateSecondaryStimulus(
      SecondarySignificantStimulusCreateDto model,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), model.DataSetId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }

      var primaryStimulus = await DbContext.SignificantStimuli
        .SingleOrDefaultAsync(x => x.Id == model.PrimaryStimulusId && x.Type == Shared.Enums.StimulusType.Primary);

      if (primaryStimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(SignificantStimulus), model.PrimaryStimulusId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }

      var stimulus = Mapper.Map<SecondarySignificantStimulusCreateDto, SignificantStimulus>(model);
      stimulus.DataSet = dataset;

      DbContext.SignificantStimuli.Add(stimulus);
      await DbContext.SaveChangesAsync(cancellationToken);

      var significantMatch = new SignificantMatch
      {
        Primary = primaryStimulus as PrimarySignificantStimulus,
        Secondary = stimulus as SecondarySignificantStimulus,
      };

      DbContext.SignificantMatches.Add(significantMatch);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<SignificantStimulus, SignificantStimulusRetrieveDto>(stimulus);
      return CreatedAtAction(nameof(RetrieveSignificantStimulus), new { id = stimulus.Id }, dto);
    }

    [HttpPost("fetch")]
    public async Task<ActionResult<PagedResult<SignificantStimulusRetrieveDto>>> RetrieveAll(
      PaginationCriteria pagination,
      CancellationToken cancellationToken = default)
    {
      var query = DbContext.SignificantStimuli;

      var total = await query.CountAsync(cancellationToken);

      var significantStimuli = await query
        .OrderBy(x => x.Id)
        .Slice(pagination.Page, pagination.ItemsPerPage)
        .Include(x => x.DataSet)
        .Project<SignificantStimulus, SignificantStimulusRetrieveDto>(Mapper)
        .ToListAsync(cancellationToken);

      var result = significantStimuli.Paginate(pagination, total);

      return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SignificantStimulusRetrieveDto>> RetrieveSignificantStimulus(
      long id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await DbContext.SignificantStimuli
        .Include(x => x.DataSet)
        .Where(x => x.Id == id)
        .Project<SignificantStimulus, SignificantStimulusRetrieveDto>(Mapper)
        .SingleOrDefaultAsync(cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(Stimulus), id);
        return NotFound();
      }

      return Ok(stimulus);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SignificantStimulusRetrieveDto>> UpdateSignificantStimulus(
      long id, SignificantStimulusUpdateDto model, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulusToBeUpdated = await DbContext.SignificantStimuli
        .Include(x => x.DataSet)
        .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulusToBeUpdated == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(SignificantStimulus), id);
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

      DbContext.SignificantStimuli.Update(stimulusToBeUpdated);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<SignificantStimulus, SignificantStimulusRetrieveDto>(stimulusToBeUpdated);

      return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSignificantStimulus(long id, CancellationToken cancellationToken)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await DbContext.SignificantStimuli.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(SignificantStimulus), id);
        return NotFound();
      }

      DbContext.SignificantStimuli.Remove(stimulus);
      await DbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
