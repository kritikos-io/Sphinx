namespace Kritikos.Sphinx.Web.CommonIdentity
{
  using System.Collections.Generic;
  using System.Security.Claims;

  public static class SphinxClaimHelpers
  {
    public const string ClaimBaseName = "permission";

    public const string CanManageSessions = "ManageSessions";

    static SphinxClaimHelpers()
    {
      Claims = new List<Claim> { new(ClaimBaseName, CanManageSessions), };
    }

    public static IReadOnlyCollection<Claim> Claims { get; }
  }
}
