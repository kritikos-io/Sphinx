namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class TestSession : SphinxEntity<Guid, TestSession>
  {
    public IReadOnlyCollection<SessionQuestion> Questions { get; set; }
      = new List<SessionQuestion>(0);

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestSession>(entity =>
      {
        OnModelCreating(entity);
      });
  }
}
