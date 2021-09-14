namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class TestSession : SphinxEntity<long, TestSession>
  {
    public string Title { get; set; }
    public StimuliGroup A { get; set; } = default!;

    public StimuliGroup B { get; set; } = default!;

    public StimuliGroup C { get; set; } = default!;

    public StimuliGroup? D { get; set; }

    public StimuliGroup? E { get; set; }

    public ICollection<TestSessionQuestion> Questions { get; set; }
      = new List<TestSessionQuestion>();

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<TestSession>(entity =>
      {
        entity.HasOne(e => e.A)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.B)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);


        entity.HasOne(e => e.C)
          .WithMany()
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.D)
          .WithMany()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.E)
          .WithMany()
          .OnDelete(DeleteBehavior.Restrict);
      });
  }
}
