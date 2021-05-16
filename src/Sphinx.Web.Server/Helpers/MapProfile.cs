namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  public static class MapProfile
  {
    public static readonly IPureMapperConfig DtoMapping = new PureMapperConfig()
      .Map<DataSet, DatasetRetrieveDto>(
        _ => dataset => new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name, })
      .Map<DatasetCreateDto, DataSet>(_ => dto => new DataSet { Name = dto.Name, })
      .Map<DatasetUpdateDto, DataSet>(mapper => (dto, entity) => UpdateDataset(entity, dto));

    private static DataSet UpdateDataset(DataSet entity, DatasetUpdateDto dto)
    {
      entity.Name = dto.Name;
      return entity;
    }
  }
}
