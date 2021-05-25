namespace Kritikos.Sphinx.Web.Shared.UpdateDto
{
  using System;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class InsignificantStimulusUpdateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid DataSetId { get; set; }
  }
}
