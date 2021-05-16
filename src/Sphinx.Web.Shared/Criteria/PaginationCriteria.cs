namespace Kritikos.Sphinx.Web.Shared.Criteria
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Class that defines the paging searches.
  /// </summary>
  public class PaginationCriteria
  {
    /// <summary>
    /// Defines the page of the results.
    /// </summary>
    /// <remarks>The starting page is 1.</remarks>
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than {1}")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Defines the items per page.
    /// </summary>
    [Range(10, 100)]
    public int ItemsPerPage { get; set; } = 10;
  }
}
