namespace Kritikos.Sphinx.Web.Server.Helpers.Extensions
{
  using System;
  using System.Collections.Generic;

  using Kritikos.Sphinx.Web.Shared;
  using Kritikos.Sphinx.Web.Shared.Criteria;

  public static class PaginationExtensions
  {
    public static PagedResult<T> Paginate<T>(this List<T> source, PaginationCriteria pagination, int totalCount)
      => new()
      {
        Results = source,
        Metadata = new()
        {
          TotalCount = totalCount,
          PageSize = pagination?.ItemsPerPage ?? throw new ArgumentNullException(nameof(pagination)),
          CurrentPage = pagination.Page,
          TotalPages = (int)Math.Ceiling(totalCount / (double)pagination.ItemsPerPage),
        },
      };
  }
}
