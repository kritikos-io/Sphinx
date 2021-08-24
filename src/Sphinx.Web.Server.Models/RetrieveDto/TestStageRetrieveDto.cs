namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using Kritikos.Sphinx.Web.Shared.Enums;

  public class TestStageRetrieveDto
  {
    public long Id { get; set; }

    public StageType Type { get; set; }

    public TestSessionRetrieveDto? TestSession { get; set; }
  }
}
