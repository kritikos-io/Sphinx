namespace Kritikos.Sphinx.Web.Client.Components
{
  using System.Collections.Generic;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Client.Helpers;
  using Kritikos.Sphinx.Web.Shared;

  using Microsoft.AspNetCore.Components;

  public partial class Pagination
  {
    private List<PagingLink> links = new();

    [Parameter]
    public PaginationMetadata Metadata { get; set; }

    [Parameter]
    public int Spread { get; set; }

    [Parameter]
    public EventCallback<int> SelectedPage { get; set; }

    public void CreatePaginationLinks()
    {
      links = new List<PagingLink>();
      links.Add(new PagingLink(Metadata.CurrentPage - 1, Metadata.HasPrevious, "Previous"));
      for (int i = 1; i <= Metadata.TotalPages; i++)
      {
        if (i >= Metadata.CurrentPage - Spread && i <= Metadata.CurrentPage + Spread)
        {
          links.Add(new PagingLink(i, true, i.ToString()) { Active = Metadata.CurrentPage == i });
        }
      }

      links.Add(new PagingLink(Metadata.CurrentPage + 1, Metadata.HasNext, "Next"));
    }

    protected override void OnParametersSet()
      => CreatePaginationLinks();

    private async Task OnSelectedPage(PagingLink link)
    {
      if (link.Page == Metadata.CurrentPage || !link.Enabled)
      {
        return;
      }

      Metadata.CurrentPage = link.Page;
      await SelectedPage.InvokeAsync(link.Page);
    }
  }
}
