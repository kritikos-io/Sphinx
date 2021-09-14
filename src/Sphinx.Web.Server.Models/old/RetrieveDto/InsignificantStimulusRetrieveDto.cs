namespace Kritikos.Sphinx.Web.Server.Models.old.RetrieveDto
{
  using Kritikos.Sphinx.Web.Server.Models.Enums;

  public class InsignificantStimulusRetrieveDto
  {
    public long Id { get; set; }

    public StimulusMediaType MediaType { get; set; }
    
    public string Content { get; set; } = string.Empty;

    public DatasetRetrieveDto? DataSet { get; set; }
  }
}
