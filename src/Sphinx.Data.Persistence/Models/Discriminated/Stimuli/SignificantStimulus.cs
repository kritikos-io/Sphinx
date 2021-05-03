namespace Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli
{
  using Microsoft.EntityFrameworkCore;

  public class SignificantStimulus : Stimulus
  {
    public string Title { get; set; } = string.Empty;

    internal static new void OnModelCreating(ModelBuilder builder)
      => builder.Entity<InsignificantStimulus>(entity =>
      {
      });
  }
}
