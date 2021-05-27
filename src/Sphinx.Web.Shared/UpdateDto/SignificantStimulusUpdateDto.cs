namespace Kritikos.Sphinx.Web.Shared.UpdateDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class SignificantStimulusUpdateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid DataSetId { get; set; }

    public string Title { get; set; } = string.Empty;
  }
}
