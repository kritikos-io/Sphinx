namespace Kritikos.Sphinx.Web.Server.Models.API
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Server.Models.CreateDto;
  using Kritikos.Sphinx.Web.Server.Models.Criteria;
  using Kritikos.Sphinx.Web.Server.Models.RetrieveDto;

  using Refit;

  public interface ISphinxApi
  {
    [Get("/WeatherForecast")]
    Task<List<WeatherForecast>> GetForecast();

    [Get("/api/testsession")]
    Task<ApiResponse<PagedResult<TestSessionRetrieveDto>>> GetTestSessions(PaginationCriteria criteria);

    [Get("/api/testsession/{id}")]
    Task<TestSessionRetrieveDto> GetTestSession(Guid id);

    [Get("/api/dataset/grouped")]
    Task<List<StimuliGroupWithDatasetDto>> GetGroupsWithDatasetTitle();

    [Post("/api/testSession")]
    Task CreateSession(TestSessionCreateDto dto);
  }
}
