namespace Kritikos.Sphinx.Web.Server.Models.old.CreateDto
{
  using System;

  public class UserSessionCreateDto
  {
    public Guid UserId { get; set; }

    public Guid TestSessionId { get; set; }
  }
}