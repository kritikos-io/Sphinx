namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using Kritikos.PureMap;
  using Kritikos.PureMap.Contracts;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Web.Server.Models.RetrieveDto;

  using Nessos.Expressions.Splicer;

  public static class MapProfile
  {
    public static readonly IPureMapperConfig DtoMapping = new PureMapperConfig()
      .Map<StimuliGroup, StimuliGroupWithDatasetDto>(_ => group => new StimuliGroupWithDatasetDto
      {
        Id = group.Id,
        Title = $"{group.Dataset.Title} - {group.Title}",
      });
  }
}
