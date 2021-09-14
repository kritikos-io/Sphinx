namespace Kritikos.Sphinx.Web.Server.Models
{
  using System;

  public abstract class SphinxDto<TKey>
    where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
  {
    public TKey Id { get; set; } = default!;
  }
}
