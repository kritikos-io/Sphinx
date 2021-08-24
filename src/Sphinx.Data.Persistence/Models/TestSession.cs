#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;
  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.EntityFrameworkCore;

  public class TestSession : SphinxEntity<Guid, TestSession>
  {
    public bool IsPublic { get; set; }

    // A, B, C, D, E
    // Insignificant-> Dataset
    // Significant -> Dataset & title
    public SphinxUser User { get; set; }

    public IReadOnlyCollection<SessionQuestion> Questions { get; set; }
      = new List<SessionQuestion>(0);

    public IReadOnlyCollection<TestStage> TestStages { get; set; }
      = new List<TestStage>(0);

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestSession>(entity =>
      {
        OnModelCreating(entity);
      });
  }
}
