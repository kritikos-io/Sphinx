namespace Kritikos.Sphinx.Data.Persistence.Models
{
  using Kritikos.Sphinx.Web.Shared.Enums;

  public abstract class Stimulus
  {
    public long Id { get; set; }

    public StimulusMediaType Type { get; set; }

    public DataSet DataSet { get; set; }

    public string Content { get; set; }

    public bool isSignificant { get; set; }
  }
}
