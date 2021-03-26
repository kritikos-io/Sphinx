namespace Kritikos.Sphinx.Data.Persistence.Helpers
{
  using System;

  using Kritikos.Configuration.Persistence.Abstractions;

  public class BaseEntity : IEntity<long>, IAuditable<Guid>, ITimestamped
  {
    public long Id { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
  }
}
