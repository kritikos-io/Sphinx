namespace Kritikos.Sphinx.Web.Server.Helpers
{
  public static class LogTemplates
  {
    public static class Generic
    {
      public const string UnhandledException = "An unhandled exception occurred: {Message}";
      public const string BootstrappingError = "Error while bootstrapping application: {Message}";
    }

    public static class Razor
    {
      public const string ErrorHandlingRequest = "Unidentified error while handling request {RequestId}";
    }

    public static class Oidc
    {
      public const string ClientParametersRequested =
        "Client {ClientId} requested parameter info and was sent {Parameters}";
    }
  }
}
