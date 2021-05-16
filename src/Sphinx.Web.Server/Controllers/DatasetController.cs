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
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Helpers;
  using Kritikos.Sphinx.Web.Server.Helpers.Extensions;
  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.Criteria;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  using Microsoft.AspNetCore.Mvc;
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

      var dataset = Mapper.Map<DatasetCreateDto, DataSet>(model);

      DbContext.DataSets.Add(dataset);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = Mapper.Map<DataSet, DatasetRetrieveDto>(dataset);

      return CreatedAtAction(nameof(RetrieveDataset), new { id = dataset.Id }, dto);
    }

    [HttpPost("fetch")]
    public async Task<ActionResult<PagedResult<DatasetRetrieveDto>>> RetrieveAll(
      PaginationCriteria pagination,
      CancellationToken cancellationToken = default)
    {
      var query = DbContext.DataSets;

      var total = await query.CountAsync(cancellationToken);

      var dataSets = await query
        .OrderBy(x => x.Id)
        .Slice(pagination.Page, pagination.ItemsPerPage)
        .Project<DataSet, DatasetRetrieveDto>(Mapper)
        .ToListAsync(cancellationToken);

      var result = dataSets.Paginate(pagination, total);

      return Ok(result);
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

      var dataset = await DbContext.DataSets
        .Where(x => x.Id == id)
        .Project<DataSet, DatasetRetrieveDto>(Mapper)
        .SingleOrDefaultAsync(cancellationToken);

      if (dataset == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      return Ok(dataset);
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

      var dataSet = await DbContext.DataSets.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

      if (dataSet == null)
      {
        Logger.LogWarning(LogTemplates.Entity.NotFound, nameof(DataSet), id);
        return NotFound();
      }

      Mapper.Map(model, dataSet);

      DbContext.DataSets.Update(dataSet);
      await DbContext.SaveChangesAsync(cancellationToken);

      var dto = new DatasetRetrieveDto { Id = dataSet.Id, Name = dataSet.Name };

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
