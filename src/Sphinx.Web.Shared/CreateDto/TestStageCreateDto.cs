namespace Kritikos.Sphinx.Web.Shared.CreateDto
{
  using System;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class TestStageCreateDto
  {
    public StageType Type { get; set; }

    public Guid TestSessionId { get; set; }
  }
}
