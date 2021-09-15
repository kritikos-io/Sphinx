namespace Kritikos.Sphinx.Web.Server.Models.RetrieveDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class TestSessionRetrieveDto : SphinxDto<long>
  {
    public string Title { get; set; } = string.Empty;
  }
}
