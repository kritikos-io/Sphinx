namespace Kritikos.Sphinx.Web.Server.Models.old.CreateDto
{
  using System;

  using Kritikos.Sphinx.Web.Server.Models.Enums;

  public class TestStageCreateDto
  {
    public StageType Type { get; set; }

    public Guid TestSessionId { get; set; }
  }
}
