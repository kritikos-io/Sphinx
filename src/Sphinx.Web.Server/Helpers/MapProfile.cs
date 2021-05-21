namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Shared.CreateDto;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Kritikos.Sphinx.Web.Shared.UpdateDto;

  using Nessos.Expressions.Splicer;

  public static class MapProfile
  {
    public static readonly IPureMapperConfig DtoMapping = new PureMapperConfig()
      .Map<DataSet, DatasetRetrieveDto>(
        _ => dataset => new DatasetRetrieveDto { Id = dataset.Id, Name = dataset.Name, })
      .Map<DatasetCreateDto, DataSet>(_ => dto => new DataSet { Name = dto.Name, })
      .Map<DatasetUpdateDto, DataSet>(mapper => (dto, entity) => UpdateDataset(entity, dto))
      .Map<InsignificantStimulus, InsignificantStimulusRetrieveDto>(mapper => stimulus =>
        new InsignificantStimulusRetrieveDto
        {
          Id = stimulus.Id,
          Content = stimulus.Content,
          MediaType = stimulus.MediaType,
          Type = stimulus.Type,
          DataSet = mapper.Resolve<DataSet, DatasetRetrieveDto>().Invoke(stimulus.DataSet),
        })
      .Map<InsignificantStimulusCreateDto, InsignificantStimulus>(mapper => dto =>
         new InsignificantStimulus
         {
           Content = dto.Content,
           MediaType = dto.MediaType,
           Type = dto.Type,
         })
      .Map<InsignificantStimulusUpdateDto, InsignificantStimulus>(mapper=>(dto, entity)=> UpdateInsignificantStimulus(entity, dto));

    private static DataSet UpdateDataset(DataSet entity, DatasetUpdateDto dto)
    {
      entity.Name = dto.Name;
      return entity;
    }

    private static InsignificantStimulus UpdateInsignificantStimulus(InsignificantStimulus entity, InsignificantStimulusUpdateDto dto)
    {
      entity.Content = dto.Content;
      entity.MediaType = dto.MediaType;
      entity.Type = dto.Type;
      return entity;
    }
  }
}
