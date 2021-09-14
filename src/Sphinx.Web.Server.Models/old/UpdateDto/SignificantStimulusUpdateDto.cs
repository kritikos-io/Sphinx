namespace Kritikos.Sphinx.Web.Server.Models.old.UpdateDto
{
  using System;

  using Kritikos.Sphinx.Web.Server.Models.Enums;

  public class SignificantStimulusUpdateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid DataSetId { get; set; }

    public string Title { get; set; } = string.Empty;
  }
}
