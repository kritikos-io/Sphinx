namespace Kritikos.Sphinx.Web.CommonIdentity
{
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Authorization;

  public static class SphinxPolicyHelper
  {
    public const string IsAdmin = "IsAdmin";
    public const string ManageSessions = "ManageSessions";

    static SphinxPolicyHelper()
    {
      Policies = new List<(string Name, AuthorizationPolicy Policy)>()
      {
        (IsAdmin, IsAdminPolicy()), (ManageSessions, CanManageSessions()),
      };
    }

    public static IReadOnlyCollection<(string Name, AuthorizationPolicy Policy)> Policies { get; }

    public static void RegisterSphinxPolicies(this AuthorizationOptions options)
    {
      foreach (var (name, policy) in Policies)
      {
        options?.AddPolicy(name, policy);
      }
    }

    public static AuthorizationPolicy IsAdminPolicy()
      => BaseRolePolicy(SphinxRoleHelper.Administrator)
        .Build();

    public static AuthorizationPolicy CanManageSessions()
      => BaseClaimPolicy(SphinxClaimHelpers.CanManageSessions)
        .Build();

    private static AuthorizationPolicyBuilder BaseRolePolicy(string roleName)
      => PolicyBase()
        .RequireRole(roleName);

    private static AuthorizationPolicyBuilder BaseClaimPolicy(string claimName)
      => PolicyBase()
        .RequireClaim(SphinxClaimHelpers.ClaimBaseName, claimName);

    private static AuthorizationPolicyBuilder PolicyBase()
      => new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser();
  }
}
