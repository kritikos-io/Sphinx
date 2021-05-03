#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using Kritikos.Sphinx.Data.Persistence.Helpers;

  using Microsoft.EntityFrameworkCore;

  public class SessionQuestion : SphinxEntity
  {
    private const string SessionId = "TestSessionId";
    private const string PrimaryId = "PrimaryStimulusId";
    private const string SecondaryId = "SecondaryStimulusId";

    public TestSession Session { get; set; }

    public Stimulus Primary { get; set; }

    public Stimulus Secondary { get; set; }

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<SessionQuestion>(entity =>
      {
        entity.HasOne(y => y.Session)
          .WithMany()
          .HasForeignKey(SessionId);
        entity.HasOne(y => y.Primary)
          .WithMany()
          .HasForeignKey(PrimaryId);
        entity.HasOne(y => y.Secondary)
          .WithMany()
          .HasForeignKey(SecondaryId);
        entity.HasKey(SessionId, PrimaryId, SecondaryId);
      });
  }
}
