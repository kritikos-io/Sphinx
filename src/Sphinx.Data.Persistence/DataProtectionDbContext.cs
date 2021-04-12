namespace Kritikos.Sphinx.Data.Persistence
{

  using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;

  public class DataProtectionDbContext : DbContext, IDataProtectionKeyContext
  {
    public DataProtectionDbContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

  }
}
