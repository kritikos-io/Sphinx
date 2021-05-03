#nullable disable
#pragma warning disable SA1402 // File may only contain a single type
namespace Kritikos.Sphinx.Data.Persistence.Helpers
{
  using System;

  using Kritikos.Configuration.Persistence.Abstractions;

  using Microsoft.EntityFrameworkCore.Metadata.Builders;

  public abstract class SphinxEntity : IAuditable<Guid>, ITimestamped
  {
    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
  }

  public abstract class SphinxEntity<TKey, TEntity> : SphinxEntity, IEntity<TKey>
    where TKey : IEquatable<TKey>, IComparable<TKey>, IComparable
    where TEntity : class, IEntity<TKey>
  {
    public TKey Id { get; set; }

    internal static void OnModelCreating(EntityTypeBuilder<TEntity> entity)
      => entity.HasKey(e => e.Id);
  }
}
