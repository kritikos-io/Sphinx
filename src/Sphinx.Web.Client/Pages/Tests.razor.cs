namespace Kritikos.Sphinx.Web.Client.Pages
{
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.API;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;

  using Microsoft.AspNetCore.Components;
  using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
  using Microsoft.Extensions.Logging;

  public partial class Tests
  {
    #region Fields
    private PagedResult<TestSessionRetrieveDto>? testSessions;
    #endregion

    #region Injects
    [Inject]
    private ISphinxApi ApiServices { get; set; }

    [Inject]
    private ILogger<FetchData> Logger { get; set; }
    #endregion

    protected override async Task OnInitializedAsync()
    {
      await Refresh();
    }

    private async Task Refresh()
    {
      try
      {
        testSessions = await ApiServices.GetTestSessions();
      }
      catch (AccessTokenNotAvailableException exception)
      {
        exception.Redirect();
      }
    }
  }
}
