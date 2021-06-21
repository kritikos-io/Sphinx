#nullable disable
namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Data.Persistence.Identity;

  using Microsoft.EntityFrameworkCore;

  public class UserSession
  {
    private const string UserId = "SphinxUserId";

    private const string TestSessionId = "TestSessionId";

    public SphinxUser User { get; set; }

    public TestSession TestSession { get; set; }

    internal static void OnModelCreating(ModelBuilder builder)
      => builder.Entity<UserSession>(entity =>
      {
        entity.HasOne(e => e.User)
          .WithMany()
          .HasForeignKey(UserId)
          .IsRequired();

        entity.HasOne(e => e.TestSession)
          .WithMany()
          .HasForeignKey(TestSessionId)
          .IsRequired();

        entity.HasKey(UserId, TestSessionId);
      });
  }
}
