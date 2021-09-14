namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class TestSessionQuestion : SphinxEntity<long, TestSessionQuestion>
  {
    public TestSession Session { get; set; } = default!;

    public Stimulus UnderTest { get; set; } = default!;

    public Stimulus CorrectAnswer { get; set; } = default!;

    public Stimulus False1 { get; set; } = default!;

    public Stimulus False2 { get; set; } = default!;

    public Stimulus False3 { get; set; } = default!;

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestSessionQuestion>(entity =>
      {
        entity.HasOne(e => e.Session)
          .WithMany(e => e.Questions)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.UnderTest)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.CorrectAnswer)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.False1)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.False2)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.False3)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);
      });
  }
}
