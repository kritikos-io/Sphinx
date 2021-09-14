namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Microsoft.EntityFrameworkCore;

  public class SignificantStimuliMatch
  {
    internal const string PrimaryId = "PrimaryStimulusId";
    internal const string SecondaryId = "SecondaryStimulusId";

    public Stimulus Primary { get; set; } = default!;

    public Stimulus Secondary { get; set; } = default!;

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<SignificantStimuliMatch>(entity =>
      {
        entity.Property<long>(PrimaryId);
        entity.Property<long>(SecondaryId);

        entity.HasOne(e => e.Primary)
          .WithMany()
          .HasForeignKey(PrimaryId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Secondary)
          .WithOne()
          .HasForeignKey(typeof(SignificantStimuliMatch), SecondaryId)
          .IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

        entity.HasKey(PrimaryId, SecondaryId);
      });
  }
}
