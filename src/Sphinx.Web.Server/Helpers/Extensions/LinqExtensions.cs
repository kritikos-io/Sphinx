namespace Kritikos.Sphinx.Web.Server.Helpers.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public static class LinqExtensions
  {
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
      Random rnd = new Random();
      return source.OrderBy<T, int>((item) => rnd.Next());
    }

    public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition,
      Func<TSource, bool> predicate)
      => condition
        ? source.Where(predicate)
        : source;
  }
}
