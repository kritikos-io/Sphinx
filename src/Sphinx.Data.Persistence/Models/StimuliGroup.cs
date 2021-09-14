namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class StimuliGroup : SphinxEntity<long, StimuliGroup>
  {
    public string Title { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public SphinxDataset Dataset { get; set; } = default!;

    public IReadOnlyCollection<Stimulus> Stimuli { get; set; }
      = new List<Stimulus>();

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<StimuliGroup>(entity =>
      {
        entity.HasOne(e => e.Dataset)
          .WithMany(e => e.Groups)
          .OnDelete(DeleteBehavior.Restrict);
      });
  }
}
