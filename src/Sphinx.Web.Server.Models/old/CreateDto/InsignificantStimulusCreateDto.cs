namespace Kritikos.Sphinx.Web.Server.Models.old.CreateDto
{
  using System;

  using Kritikos.Sphinx.Web.Server.Models.Enums;

  public class InsignificantStimulusCreateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid DataSetId { get; set; }
  }
}
