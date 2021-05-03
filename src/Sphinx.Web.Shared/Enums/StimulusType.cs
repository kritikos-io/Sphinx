namespace Kritikos.Sphinx.Web.Shared.Enums
{
  using System;

  [Flags]
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
