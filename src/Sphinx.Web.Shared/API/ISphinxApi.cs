namespace Kritikos.Sphinx.Web.Shared.API
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared.Criteria;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Refit;

  public interface ISphinxApi
  {
    [Get("/WeatherForecast")]
    Task<List<WeatherForecast>> GetForecast();

    [Get("/api/testsession")]
    Task<PagedResult<TestSessionRetrieveDto>> GetTestSessions(PaginationCriteria criteria);

    [Get("/api/testsession/{id}")]
    Task<TestSessionRetrieveDto> GetTestSession(Guid id);
  }
}
