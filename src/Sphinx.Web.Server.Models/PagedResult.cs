#pragma warning disable SA1402 // File may only contain a single type
#pragma warning disable CA2227 // Collection properties should be read only
namespace Kritikos.Sphinx.Web.Shared
{
  using System.Collections.Generic;

  public class PagedResult<T>
  {
    public ICollection<T> Results { get; set; }
      = new List<T>();

    public PaginationMetadata Metadata { get; set; } = new();
  }

  public class PaginationMetadata
  {
    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;
  }
}
