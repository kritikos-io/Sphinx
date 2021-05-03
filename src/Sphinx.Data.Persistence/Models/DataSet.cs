namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class DataSet : SphinxEntity<Guid, DataSet>
  {
    public string Name { get; set; } = string.Empty;

    public IReadOnlyCollection<Stimulus> Stimuli { get; set; }
      = new List<Stimulus>(0);

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<DataSet>(entity => { OnModelCreating(entity); });
  }
}
