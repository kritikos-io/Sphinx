#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;

  using Microsoft.EntityFrameworkCore;

  public class SignificantMatch
  {
    private const string PrimaryId = "PrimaryStimulusId";
    private const string SecondaryId = "SecondaryStimulusId";

    public PrimarySignificantStimulus Primary { get; set; }

    public SecondarySignificantStimulus Secondary { get; set; }

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<SignificantMatch>(entity =>
      {
        entity.HasOne(e => e.Primary)
          .WithMany(e => e.Matches)
          .HasForeignKey(PrimaryId)
          .IsRequired();

        entity.HasOne(e => e.Secondary)
          .WithOne(e => e.Match)
          .HasForeignKey(typeof(SignificantMatch), SecondaryId)
          .IsRequired();

        entity.HasKey(PrimaryId, SecondaryId);
      });
  }
}
