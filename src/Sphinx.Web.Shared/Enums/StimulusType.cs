namespace Kritikos.Sphinx.Web.Shared.Enums
{
  public enum StimulusType
  {
    None = 0,

    // Base Type
    Significant = 1,
    Insignificant = Significant << 1,

    // Matches type
    Primary = Insignificant << 1,
    Secondary = Primary << 1,
  }
}
