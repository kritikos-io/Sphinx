namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System.Linq;
  using System.Security.Cryptography.X509Certificates;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
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
    private readonly SphinxDbContext dbContext;

    public InsignificantStimuliController(
      ILogger<InsignificantStimuliController> logger,
      SphinxDbContext dbContext)
      : base(logger)
    {
      this.dbContext = dbContext;
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

      var datasetIds = await dbContext.DataSets.Select(x => x.Id).ToListAsync(cancellationToken);
      var dataset = await dbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), model.DataSetId);
        return NotFound("The dataset you want to add this stimulus does not exist");
      }

      var stimulus = new InsignificantStimulus()
      {
        MediaType = model.MediaType, Type = model.Type, Content = model.Content, DataSet = dataset,
      };

      dbContext.InsignificantStimuli.Add(stimulus);
      await dbContext.SaveChangesAsync(cancellationToken);

      var dto = new InsignificantStimulusRetrieveDto()
      {
        Id = stimulus.Id,
        Content = stimulus.Content,
        MediaType = stimulus.MediaType,
        Type = stimulus.Type,
        DataSet = new DatasetRetrieveDto()
        {
          Id = stimulus.DataSet.Id,
          Name = stimulus.DataSet.Name,
        },
      };

      return CreatedAtAction(nameof(RetrieveInsignificantStimulus), new { id = stimulus.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InsignificantStimulusRetrieveDto>> RetrieveInsignificantStimulus(
      long id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await dbContext.InsignificantStimuli
        .Include(x => x.DataSet)
        .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(Stimulus), id);
        return NotFound();
      }

      var dto = new InsignificantStimulusRetrieveDto()
      {
        Id = stimulus.Id,
        Content = stimulus.Content,
        MediaType = stimulus.MediaType,
        Type = stimulus.Type,
        DataSet = new DatasetRetrieveDto()
        {
          Id = stimulus.DataSet.Id,
          Name = stimulus.DataSet.Name,
        },
      };

      return Ok(dto);
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

      var stimulusToBeUpdated = await dbContext.InsignificantStimuli
        .Include(x => x.DataSet)
        .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (stimulusToBeUpdated == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(InsignificantStimulus), id);
        return NotFound();
      }

      if (model.DataSetId != stimulusToBeUpdated.DataSet.Id)
      {
        var newDataSet = await dbContext.DataSets.SingleOrDefaultAsync(x => x.Id == model.DataSetId, cancellationToken);
        if (newDataSet == null)
        {
          return NotFound("The dataset you want to move the stimulus to does not exist");
        }

        stimulusToBeUpdated.DataSet = newDataSet;
      }

      stimulusToBeUpdated.MediaType = model.MediaType;
      stimulusToBeUpdated.Type = model.Type;
      stimulusToBeUpdated.Content = model.Content;

      dbContext.InsignificantStimuli.Update(stimulusToBeUpdated);
      await dbContext.SaveChangesAsync(cancellationToken);

      var dto = new InsignificantStimulusRetrieveDto()
      {
        Id = stimulusToBeUpdated.Id,
        Content = stimulusToBeUpdated.Content,
        MediaType = stimulusToBeUpdated.MediaType,
        Type = stimulusToBeUpdated.Type,
        DataSet = new DatasetRetrieveDto()
        {
          Id = stimulusToBeUpdated.DataSet.Id,
          Name = stimulusToBeUpdated.DataSet.Name,
        },
      };

      return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteInsignificantStimulus(long id, CancellationToken cancellationToken=default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var stimulus = await dbContext.InsignificantStimuli.SingleOrDefaultAsync(x => x.Id == id,cancellationToken);

      if (stimulus == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(InsignificantStimulus), id);
        return NotFound();
      }

      dbContext.InsignificantStimuli.Remove(stimulus);
      await dbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
