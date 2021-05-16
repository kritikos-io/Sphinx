namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.PureMap.Contracts;
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
    public DatasetController(SphinxDbContext dbContext, IPureMapper mapper, ILogger<DatasetController> logger)
      : base(dbContext, mapper, logger)
    {
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

      DbContext.DataSets.Add(dataset);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name };
      return CreatedAtAction(nameof(RetrieveDataset), new { id = dataset.Id }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DatasetRetrieveDto>> RetrieveDataset(
      Guid id,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var dataset = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      var dto = new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name };

      return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DatasetRetrieveDto>> UpdateDataset(
      Guid id,
      DatasetUpdateDto model,
      CancellationToken cancellationToken = default)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState.Values);
      }

      var datasetToBeUpdated = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (datasetToBeUpdated == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      datasetToBeUpdated.Name = model.Name;

      DbContext.DataSets.Update(datasetToBeUpdated);
      await DbContext.SaveChangesAsync(cancellationToken);

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

      var dataset = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      DbContext.DataSets.Remove(dataset);
      await DbContext.SaveChangesAsync(cancellationToken);

      return Ok();
    }
  }
}
