namespace Kritikos.Sphinx.Data.Persistence.Identity
{
  using System;

  using Kritikos.Configuration.Persistence.Abstractions;

  using Microsoft.AspNetCore.Identity;

  public class SphinxRole : IdentityRole<Guid>, IEntity<Guid>, IAuditable<Guid>, ITimestamped
  {
    /// <inheritdoc />
    public Guid CreatedBy { get; set; }

    /// <inheritdoc />
    public Guid UpdatedBy { get; set; }

    /// <inheritdoc />
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset UpdatedAt { get; set; }
  }
}
