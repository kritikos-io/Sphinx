namespace Kritikos.Sphinx.Web.Server.Models.Enums
{
  using System;

  [Flags]
  public enum StimulusMediaType
  {
    None = 0,
    Text = 1,
    Photo = Text << 1,
    Audio = Photo << 1,
    Video = Audio << 1,
  }
}
