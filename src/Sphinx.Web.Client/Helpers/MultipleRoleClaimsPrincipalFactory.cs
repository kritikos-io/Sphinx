namespace Kritikos.Sphinx.Web.Client.Helpers
{
  using System.Linq;
  using System.Security.Claims;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
  using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

  public class MultipleRoleClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
  {
    public MultipleRoleClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
      : base(accessor)
    {
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
      RemoteUserAccount account,
      RemoteAuthenticationUserOptions options)
    {
      var user = await base.CreateUserAsync(account, options);
      var claimsIdentity = (ClaimsIdentity)user.Identity!;
      if (account != null)
      {
        MapArrayClaimsToMultipleSeparateClaims(account, claimsIdentity);
      }

      return user;
    }

    private static void MapArrayClaimsToMultipleSeparateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
    {
      foreach (var prop in account.AdditionalProperties)
      {
        var key = prop.Key;
        var value = prop.Value;
        if (value is JsonElement { ValueKind: JsonValueKind.Array } element)
        {
          claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(prop.Key));
          var claims = element.EnumerateArray()
            .Select(x => new Claim(prop.Key, x.ToString()!));
          claimsIdentity.AddClaims(claims);
        }
      }
    }
  }
}
