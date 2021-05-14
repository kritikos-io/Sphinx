namespace Kritikos.Sphinx.Web.Shared.CreateDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class InsignificantStimulusCreateDto
  {
    public StimulusMediaType MediaType { get; set; }

    public StimulusType Type { get; set; }

    public string Content { get; set; }

    public Guid DataSetId { get; set; }
  }
}
