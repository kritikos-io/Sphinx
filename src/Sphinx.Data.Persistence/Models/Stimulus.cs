namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System.Collections.Generic;

  using Kritikos.Sphinx.Data.Persistence.Helpers;
  using Kritikos.Sphinx.Web.Server.Models.Enums;

  using Microsoft.EntityFrameworkCore;

  public class Stimulus : SphinxEntity<long, Stimulus>
  {
    public StimulusMediaType MediaType { get; set; }

    public string Content { get; set; } = string.Empty;

    public StimuliGroup Group { get; set; } = default!;

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<Stimulus>(entity =>
      {
        entity.HasOne(e => e.Group)
          .WithMany(e => e.Stimuli)
          .OnDelete(DeleteBehavior.Restrict);
      });
  }
}
