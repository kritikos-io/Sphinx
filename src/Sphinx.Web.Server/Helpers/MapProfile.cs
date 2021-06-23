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
           Type = Shared.Enums.StimulusType.Insignificant,
         })
      .Map<InsignificantStimulusUpdateDto, InsignificantStimulus>(mapper => (dto, entity) => UpdateInsignificantStimulus(entity, dto))
      .Map<SignificantStimulus, SignificantStimulusRetrieveDto>(mapper => stimulus =>
        new SignificantStimulusRetrieveDto
        {
          Id = stimulus.Id,
          Content = stimulus.Content,
          MediaType = stimulus.MediaType,
          Type = stimulus.Type,
          DataSet = mapper.Resolve<DataSet, DatasetRetrieveDto>().Invoke(stimulus.DataSet),
          Title = stimulus.Title,
        })
      .Map<SignificantStimulusUpdateDto, SignificantStimulus>(mapper => (dto, entity) => UpdateSignificantStimulus(entity, dto))
      .Map<PrimarySignificantStimulusCreateDto, SignificantStimulus>(mapper => dto =>
         new SignificantStimulus
         {
           Content = dto.Content,
           MediaType = dto.MediaType,
           Type = Shared.Enums.StimulusType.Significant | Shared.Enums.StimulusType.Primary,
           Title = dto.Title,
         })
      .Map<SecondarySignificantStimulus, SignificantStimulus>(mapper => dto =>
          new SignificantStimulus
          {
            Content = dto.Content,
            MediaType = dto.MediaType,
            Type = Shared.Enums.StimulusType.Significant | Shared.Enums.StimulusType.Secondary,
            Title = dto.Title,
          })
      .Map<TestSession, TestSessionRetrieveDto>(mapper => test =>
         new TestSessionRetrieveDto
         {
           Id = test.Id,
         });

    private static DataSet UpdateDataset(DataSet entity, DatasetUpdateDto dto)
    {
      entity.Name = dto.Name;
      return entity;
    }

    private static InsignificantStimulus UpdateInsignificantStimulus(InsignificantStimulus entity, InsignificantStimulusUpdateDto dto)
    {
      entity.Content = dto.Content;
      entity.MediaType = dto.MediaType;
      return entity;
    }

    private static SignificantStimulus UpdateSignificantStimulus(SignificantStimulus entity, SignificantStimulusUpdateDto dto)
    {
      entity.Content = dto.Content;
      entity.MediaType = dto.MediaType;
      entity.Title = dto.Title;
      return entity;
    }
  }
}
