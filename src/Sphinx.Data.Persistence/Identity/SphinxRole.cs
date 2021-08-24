namespace Kritikos.Sphinx.Data.Persistence.Identity
{
  using System;

  using Kritikos.Configuration.Persistence.Contracts.Behavioral;

  using Microsoft.AspNetCore.Identity;

  public class SphinxRole : IdentityRole<Guid>, IEntity<Guid>, IAuditable<Guid>, ITimestamped
  {
    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
  }
}
