namespace Kritikos.Sphinx.Data.Persistence.Models
{
  public enum StimulusType
  {
    None,
    Text,
    Image,
    Audio,
    Video,
  }

  public abstract class Stimulus
  {
    public long Id { get; set; }

    public StimulusType Type { get; set; }

    public DataSet DataSet { get; set; }

    public string Content { get; set; }

    public bool isSignificant { get; set; }
  }
}
