namespace Kritikos.Sphinx.Web.Server.Helpers.Extensions
{
  using System.Collections.Generic;

  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.Criteria;

  public static class PaginationExtensions
  {
    public static PagedResult<T> Paginate<T>(this List<T> source, PaginationCriteria pagination, int totalCount)
      => new()
      {
        Results = source,
        Page = pagination.Page,
        TotalElements = totalCount,
        TotalPages = (totalCount / pagination.ItemsPerPage) + 1,
      };
  }
}
