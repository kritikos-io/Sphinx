namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class TestSession : SphinxEntity<Guid, TestSession>
  {
    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestSession>(entity =>
      {
        OnModelCreating(entity);
      });
  }
}
