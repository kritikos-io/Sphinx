namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [Route("api/dataset")]
  [ApiController]
  public class DatasetController : BaseController<DatasetController>
  {
    private readonly SphinxDbContext dbContext;

    public DatasetController(ILogger<DatasetController> logger, SphinxDbContext dbContext)
        : base(logger)
    {
      this.dbContext = dbContext;
    }

    [HttpPost("")]
    public async Task<ActionResult<DatasetRetrieveDto>> CreateDataset(
        DatasetCreateDto model,
        CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = new DataSet() { Name = model.Name, };

      dbContext.DataSets.Add(dataset);
      await dbContext.SaveChangesAsync(cancellationToken);

      var dto = new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name };
      return CreatedAtAction(nameof(RetrieveDataset), new { id = dataset.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DatasetRetrieveDto>> RetrieveDataset(Guid id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = await dbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      var dto = new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name };

      return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DatasetRetrieveDto>> UpdateDataset(Guid id, DatasetUpdateDto model, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var datasetToBeUpdated = await dbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (datasetToBeUpdated == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      datasetToBeUpdated.Name = model.Name;

      dbContext.DataSets.Update(datasetToBeUpdated);
      await dbContext.SaveChangesAsync(cancellationToken);

      var dto = new DatasetRetrieveDto { Id = datasetToBeUpdated.Id, Name = datasetToBeUpdated.Name };

      return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDataset(Guid id, CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = await dbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      dbContext.DataSets.Remove(dataset);
      await dbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
