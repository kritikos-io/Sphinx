#pragma warning disable CA1034 // Nested types should not be visible
namespace Kritikos.Sphinx.Web.Server.Helpers
{
  public static class LogTemplates
  {
    public static class GenericMessages
    {
      public const string UnhandledException = "An unhandled exception occurred: {Message}";
      public const string BootstrappingError = "Error while bootstrapping application: {Message}";
    }

    public static class RazorMessages
    {
      public const string ErrorHandlingRequest = "Unidentified error while handling request {RequestId}";
    }

    public static class OidcMessages
    {
      public const string ClientParametersRequested =
        "Client {ClientId} requested parameter info and was sent {Parameters}";
    }

    public static class EntityMessages
    {
      public const string NotFound = "Requested {Entity} with id {Id}. Could not be located.";
    }

    public static class EmailMessages
    {
      public const string SendSuccess = "Succesfully sent {Subject} to {Recipient}";
      public const string SendFailure = "Failed sending {Subject} to {Recipient} with error: {Error}";
    }
  }
}
