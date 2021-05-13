namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class DatasetRetrieveDto
  {
    public string Name { get; set; }

    public Guid Id { get; set; }
  }
}
