namespace Kritikos.Sphinx.Data.Persistence.Identity
{
  using System;

  using Kritikos.Configuration.Persistence.Contracts.Behavioral;

  using Microsoft.AspNetCore.Identity;

  public class SphinxUser : IdentityUser<Guid>, IEntity<Guid>, ITimestamped
  {
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
  }
}
