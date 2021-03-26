namespace Kritikos.Sphinx.Data.Persistence.Identity
{
  using System;

  using Kritikos.Configuration.Persistence.Abstractions;

  using Microsoft.AspNetCore.Identity;

  public class SphinxUser : IdentityUser<Guid>, IEntity<Guid>, ITimestamped
  {
    /// <inheritdoc />
    public DateTimeOffset CreatedAt { get; set; }

    /// <inheritdoc />
    public DateTimeOffset UpdatedAt { get; set; }
  }
}
