namespace Kritikos.Sphinx.Web.Client.Pages
{
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.API;
  using Kritikos.Sphinx.Web.Shared.RetrieveDto;

  using Microsoft.AspNetCore.Components;
  using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

  public partial class Tests
  {
    #region Injects
    [Inject]
    private ISphinxApi apiServices { get; set; }
    #endregion

    #region Fields
    private PagedResult<TestSessionRetrieveDto> _testSessions;
    #endregion

    protected override async Task OnInitializedAsync()
    {
        await Refresh();
    }

    private async Task Refresh()
    {
      _testSessions = await apiServices.GetTestSessions();
    }
  }
}
