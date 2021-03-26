namespace Kritikos.Sphinx.Web.Shared.API
{
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Refit;

  public interface ISphinxApi
  {
    [Get("/WeatherForecast")]
    Task<List<WeatherForecast>> GetForecast();
  }
}
