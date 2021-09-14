namespace Kritikos.Sphinx.Web.Client.Helpers
{
  using System.Linq;
  using System.Security.Claims;
  using System.Text.Json;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
  using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
  using Microsoft.Extensions.Logging;

  public class MultipleRoleClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
  {
    private readonly ILogger<MultipleRoleClaimsPrincipalFactory> logger;

    public MultipleRoleClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor,
      ILogger<MultipleRoleClaimsPrincipalFactory> logger)
      : base(accessor)
    {
      this.logger = logger;
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

    private void MapArrayClaimsToMultipleSeparateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
    {
      logger.LogDebug("Adding claims to account {User}", claimsIdentity);
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
