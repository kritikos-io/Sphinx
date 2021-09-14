namespace Kritikos.Sphinx.Web.Server.Controllers
{
  using Kritikos.Sphinx.Web.Server.Helpers;

  using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  [AllowAnonymous]
  public class OidcConfigurationController : Controller
  {
    private readonly ILogger<OidcConfigurationController> logger;

    public OidcConfigurationController(
      IClientRequestParametersProvider clientRequestParametersProvider,
      ILogger<OidcConfigurationController> logger)
    {
      ClientRequestParametersProvider = clientRequestParametersProvider;
      this.logger = logger;
    }

    public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

    [HttpGet("_configuration/{clientId}")]
    public IActionResult GetClientRequestParameters([FromRoute] string clientId)
    {
      var parameters = ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId);
      logger.LogDebug(LogTemplates.OidcMessages.ClientParametersRequested, clientId, parameters);
      return Ok(parameters);
    }
  }
}
