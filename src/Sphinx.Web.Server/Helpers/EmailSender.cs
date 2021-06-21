#pragma warning disable SA1402 // File may only contain a single type
namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Identity.UI.Services;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using SendGrid;
  using SendGrid.Helpers.Mail;

  using Serilog;

  public class EmailSender : IEmailSender
  {
    public EmailSender(IOptions<SendGridOptions> optionsAccessor, ILogger<EmailSender> logger)
    {
      Logger = logger;
      Options = optionsAccessor.Value;
      Sender = new SendGridClient(Options.ApiKey);
    }

    private ILogger<EmailSender> Logger { get; }

    public SendGridOptions Options { get; }

    public SendGridClient Sender { get; }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      var msg = new SendGridMessage
      {
        From = new EmailAddress(Options.SenderMail, Options.Sender),
        Subject = subject,
        PlainTextContent = htmlMessage,
        HtmlContent = htmlMessage,
      };

      msg.AddTo(new EmailAddress(email));
      msg.SetClickTracking(false, false);

      await Sender.SendEmailAsync(msg);
    }
  }

  public class DummyEmailSender : IEmailSender
  {
    public DummyEmailSender(ILogger<DummyEmailSender> logger)
    {
      Logger = logger;
    }

    private ILogger<DummyEmailSender> Logger { get; }

    /// <inheritdoc />
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      Logger.LogInformation("Email with subject {Subject} was sent to {Email}: {Content}", subject, email, htmlMessage);
      return Task.CompletedTask;
    }
  }

  public class SendGridOptions
  {
    public string Sender { get; set; } = string.Empty;

    public string SenderMail { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;
  }
}
