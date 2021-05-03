#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli
{
  using Microsoft.EntityFrameworkCore;

  public class SecondarySignificantStimulus : SignificantStimulus
  {
    public SignificantMatch Match { get; set; }

    internal static new void OnModelCreating(ModelBuilder builder)
      => builder.Entity<SecondarySignificantStimulus>(entity =>
      {
      });
  }
}
