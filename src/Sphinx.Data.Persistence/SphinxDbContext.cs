namespace Kritikos.Sphinx.Data.Persistence
{
  using System;

  using Kritikos.Configuration.Peristence.IdentityServer;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.EntityFrameworkCore;

  public class SphinxDbContext : ApiAuthorizationDbContext<SphinxUser, SphinxRole, Guid>
  {
    public SphinxDbContext(DbContextOptions options)
      : base(options)
    {
    }
  }
}
