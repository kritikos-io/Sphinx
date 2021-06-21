namespace Kritikos.Sphinx.Web.Shared.API
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared.RetrieveDto;
  using Refit;

  public interface ISphinxApi
  {
    [Get("/WeatherForecast")]
    Task<List<WeatherForecast>> GetForecast();

    [Get("api/testsession")]
    Task<PagedResult<TestSessionRetrieveDto>> GetTestSessions();

    [Get("api/testsession/{id}")]
    Task<TestSessionRetrieveDto> GetTestSession(Guid id, CancellationToken cancellationToken = default);
  }
}
