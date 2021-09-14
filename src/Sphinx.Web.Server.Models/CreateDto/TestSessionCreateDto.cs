namespace Kritikos.Sphinx.Web.Server.Models.CreateDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class TestSessionCreateDto
  {
    public string Title { get; set; } = string.Empty;

    public long GroupA { get; set; }

    public long GroupB { get; set; }

    public long GroupC { get; set; }

    public long? GroupD { get; set; }

    public long? GroupE { get; set; }
  }
}
