namespace Kritikos.Sphinx.Web.Shared.Enums
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public enum StageType
  {
    Unknown = 0,
    AB = 1,
    BA = AB << 1,
    AC = BA << 1,
    CA = AC << 1,
    BC = CA << 1,
    CB = BC << 1,
    AD = CB << 1,
    DA = AD << 1,
    CD = DA << 1,
    DC = CD << 1,
  }
}
