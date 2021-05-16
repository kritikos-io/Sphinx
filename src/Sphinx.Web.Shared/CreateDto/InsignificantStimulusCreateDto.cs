namespace Kritikos.Sphinx.Web.Shared.CreateDto
{
  using System;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class InsignificantStimulusCreateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public StimulusType Type { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid DataSetId { get; set; }
  }
}
