namespace Kritikos.Sphinx.Web.Shared
{
  using System.Collections.Generic;

  public class PagedResult<T>
  {
    public List<T> Results { get; set; }
      = new();

    public int Page { get; set; }

    public int TotalPages { get; set; }

    public int TotalElements { get; set; }
  }
}
