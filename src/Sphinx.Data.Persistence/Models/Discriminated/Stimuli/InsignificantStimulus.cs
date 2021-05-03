namespace Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli
{
  using Microsoft.EntityFrameworkCore;

  public class InsignificantStimulus : Stimulus
  {
    internal static new void OnModelCreating(ModelBuilder builder)
      => builder.Entity<InsignificantStimulus>(entity =>
      {
      });
  }
}
