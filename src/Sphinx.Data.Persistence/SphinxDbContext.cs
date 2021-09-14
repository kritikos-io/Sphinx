#nullable disable
namespace Kritikos.Sphinx.Data.Persistence
{
  using System;

  using Kritikos.Configuration.Peristence.IdentityServer;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;

  using Microsoft.EntityFrameworkCore;

  public class SphinxDbContext : ApiAuthorizationDbContext<SphinxUser, SphinxRole, Guid>
  {
    public SphinxDbContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<SphinxDataset> Datasets { get; set; }

    public DbSet<StimuliGroup> StimulusGroups { get; set; }

    public DbSet<Stimulus> Stimuli { get; set; }

    public DbSet<SignificantStimuliMatch> SignificantMatches { get; set; }

    public DbSet<TestSession> TestSessions { get; set; }

    public DbSet<TestSessionQuestion> SessionQuestions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      SphinxDataset.OnModelCreating(builder);
      StimuliGroup.OnModelCreating(builder);
      Stimulus.OnModelCreating(builder);
      SignificantStimuliMatch.OnModelCreating(builder);
      TestSession.OnModelCreating(builder);
      TestSessionQuestion.OnModelCreating(builder);
    }
  }
}
