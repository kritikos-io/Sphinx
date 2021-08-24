namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using Kritikos.Sphinx.Web.Shared.Enums;

  public class InsignificantStimulusRetrieveDto
  {
    public long Id { get; set; }

    public StimulusMediaType MediaType { get; set; }

    public StimulusType Type { get; set; }

    public string Content { get; set; } = string.Empty;

    public DatasetRetrieveDto? DataSet { get; set; }
  }
}
