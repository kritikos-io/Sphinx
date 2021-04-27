namespace Kritikos.Sphinx.Data.Persistence.Models
{
  public class SessionQuestion
  {
    public TestSession Session { get; set; }

    public Stimulus Primary { get; set; }

    public Stimulus Secondary { get; set; }
  }
}
