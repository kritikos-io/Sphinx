namespace Kritikos.Sphinx.Data.Persistence
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Configuration.Peristence.IdentityServer;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.EntityFrameworkCore;

  public class SphinxDbContext : ApiAuthorizationPooledDbContext<SphinxUser, SphinxRole, Guid>
  {
    public SphinxDbContext(DbContextOptions options)
      : base(options)
    {
    }
  }
}
