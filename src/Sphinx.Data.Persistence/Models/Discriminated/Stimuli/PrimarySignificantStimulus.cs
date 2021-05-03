namespace Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli
{
  using System.Collections.Generic;

  using Microsoft.EntityFrameworkCore;

  public class PrimarySignificantStimulus : SignificantStimulus
  {
    public IReadOnlyCollection<SignificantMatch> Matches { get; set; }
      = new List<SignificantMatch>(0);

    internal static new void OnModelCreating(ModelBuilder builder)
      => builder.Entity<PrimarySignificantStimulus>(entity =>
      {
      });
  }
}
