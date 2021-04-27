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

    public DbSet<Stimulus> Stimuli { get; set; }

    public DbSet<DataSet> DataSets { get; set; }

    public DbSet<TestSession> TestSessions { get; set; }

    public DbSet<SessionQuestion> SessionQuestions { get; set; }

    public DbSet<InsignificantStimulus> InsignificantStimuli { get; set; }

    public DbSet<SignificantStimulus> SignificantStimuli { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Stimulus>(x =>
      {
        x.HasDiscriminator(y => y.isSignificant)
            .HasValue<SignificantStimulus>(true)
            .HasValue<InsignificantStimulus>(false)
            .IsComplete();
        x.Property(y => y.Type)
            .HasConversion<string>();
      });

      modelBuilder.Entity<SessionQuestion>(x =>
      {
        x.HasOne(y => y.Session)
            .WithMany()
            .HasForeignKey("TestSessionId");
        x.HasOne(y => y.Primary)
            .WithMany()
            .HasForeignKey("PrimaryId");
        x.HasOne(y => y.Secondary)
            .WithMany()
            .HasForeignKey("SecondaryId");
        x.HasKey("TestSessionId", "PrimaryId", "SecondaryId");
      });
    }
  }
}
