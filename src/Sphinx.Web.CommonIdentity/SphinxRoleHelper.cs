namespace Kritikos.Sphinx.Web.CommonIdentity
{
  using System.Collections.Generic;

  public static class SphinxRoleHelper
  {
    public const string Administrator = "Administrator";
    public const string Participant = "Participant";

    static SphinxRoleHelper()
    {
      Roles = new List<string> { Administrator, Participant, };
    }

    public static IReadOnlyCollection<string> Roles { get; }
  }
}
