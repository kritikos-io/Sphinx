#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;
  using Kritikos.Sphinx.Web.Shared.Enums;

  using Microsoft.EntityFrameworkCore;

  public class TestStage : SphinxEntity<long, TestStage>
  {
    public StageType Type { get; set; }

    public TestSession TestSession { get; set; }

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestStage>(entity =>
      {
        OnModelCreating(entity);

        entity.HasOne(e => e.TestSession)
        .WithMany(e => e.TestStages)
        .IsRequired();

        entity.Property(e => e.Type)
          .HasConversion<string>();
      });
  }
}
