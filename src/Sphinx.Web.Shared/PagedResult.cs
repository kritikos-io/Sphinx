namespace Kritikos.Sphinx.Web.Shared
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class PagedResult<T>
  {
    public List<T> Results { get; set; }
      = new();

    public int Page { get; set; }

    public int TotalPages { get; set; }

    public int TotalElements { get; set; }
  }
}
