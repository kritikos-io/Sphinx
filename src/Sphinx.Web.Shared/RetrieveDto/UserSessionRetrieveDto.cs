namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class UserSessionRetrieveDto
  {
    public Guid UserId { get; set; }

    public Guid TestSessionId { get; set; }
  }
}
