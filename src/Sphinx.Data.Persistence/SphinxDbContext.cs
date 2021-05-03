namespace Kritikos.Sphinx.Data.Persistence
{
  using System;

  using Kritikos.Configuration.Peristence.IdentityServer;
  using Kritikos.Sphinx.Data.Persistence.Identity;
  using Kritikos.Sphinx.Data.Persistence.Models;
  using Kritikos.Sphinx.Data.Persistence.Models.Discriminated.Stimuli;

  using Microsoft.EntityFrameworkCore;

  public class SphinxDbContext : ApiAuthorizationDbContext<SphinxUser, SphinxRole, Guid>
  {
    public SphinxDbContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<Stimulus> Stimuli { get; set; }

    public DbSet<DataSet> DataSets { get; set; }

    public DbSet<TestSession> TestSessions { get; set; }

    public DbSet<SessionQuestion> SessionQuestions { get; set; }

    public DbSet<InsignificantStimulus> InsignificantStimuli { get; set; }

    public DbSet<SignificantStimulus> SignificantStimuli { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      DataSet.OnModelCreating(builder);

      Stimulus.OnModelCreating(builder);
      InsignificantStimulus.OnModelCreating(builder);
      SignificantStimulus.OnModelCreating(builder);

      TestSession.OnModelCreating(builder);
      SessionQuestion.OnModelCreating(builder);
    }
  }
}
