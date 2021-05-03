#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using Kritikos.Sphinx.Data.Persistence.Helpers;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;
  using Kritikos.Sphinx.Web.Shared.Enums;

  using Microsoft.EntityFrameworkCore;

  public abstract class Stimulus : SphinxEntity<long, Stimulus>
  {
    public StimulusMediaType MediaType { get; set; }

    public StimulusType Type { get; set; }

    public string Content { get; set; } = string.Empty;

    public DataSet DataSet { get; set; }

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<Stimulus>(entity =>
      {
        OnModelCreating(entity);

        entity.HasOne(e => e.DataSet)
          .WithMany(e => e.Stimuli)
          .IsRequired();

        entity.HasDiscriminator(e => e.Type)
          .HasValue<SignificantStimulus>(StimulusType.Significant)
          .HasValue<InsignificantStimulus>(StimulusType.Insignificant)
          .IsComplete();

        entity.Property(e => e.Type)
          .HasConversion<string>();

        entity.Property(e => e.MediaType)
          .HasConversion<string>();
      });
  }
}
