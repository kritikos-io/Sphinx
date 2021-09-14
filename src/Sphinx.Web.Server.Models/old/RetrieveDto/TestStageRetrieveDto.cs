namespace Kritikos.Sphinx.Web.Server.Models.old.RetrieveDto
{
  using Kritikos.Sphinx.Web.Server.Models.Enums;

  public class TestStageRetrieveDto
  {
    public long Id { get; set; }

    public StageType Type { get; set; }

    public TestSessionRetrieveDto? TestSession { get; set; }
  }
}
