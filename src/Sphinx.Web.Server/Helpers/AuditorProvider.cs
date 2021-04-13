namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using System;
  using System.Security.Claims;

  using Kritikos.Configuration.Persistence.Services;

  using Microsoft.AspNetCore.Http;

  public class AuditorProvider : IAuditorProvider<Guid>
  {
    private readonly IHttpContextAccessor accessor;

    public AuditorProvider(IHttpContextAccessor httpContextAccessor)
    {
      accessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public Guid GetAuditor() =>
      Guid.TryParse(accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var guid)
        ? guid
        : GetFallbackAuditor();

    /// <inheritdoc />
    public Guid GetFallbackAuditor() => new("195ab048-e818-5133-a6cf-4868a8a31d61");
  }
}
