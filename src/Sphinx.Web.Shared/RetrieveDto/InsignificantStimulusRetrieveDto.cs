namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  using Kritikos.Sphinx.Web.Shared.Enums;

  public class InsignificantStimulusRetrieveDto
  {
    public long Id { get; set; }

    public StimulusMediaType MediaType { get; set; }

    public StimulusType Type { get; set; }

    public string Content { get; set; }

    public DatasetRetrieveDto DataSet { get; set; }
  }
}
