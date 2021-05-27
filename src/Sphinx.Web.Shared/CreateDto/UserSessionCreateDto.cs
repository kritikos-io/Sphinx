namespace Kritikos.Sphinx.Web.Shared.CreateDto
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class UserSessionCreateDto
  {
    public Guid UserId { get; set; }

    public Guid TestSessionId { get; set; }
  }
}
