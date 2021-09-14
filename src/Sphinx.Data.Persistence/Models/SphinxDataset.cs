namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class SphinxDataset : SphinxEntity<long, SphinxDataset>
  {
    public string Title { get; set; } = string.Empty;

    public bool IsSignificant { get; set; }

    public IReadOnlyCollection<StimuliGroup> Groups { get; set; }
      = new List<StimuliGroup>();

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<SphinxDataset>(entity =>
      {
      });
  }
}
