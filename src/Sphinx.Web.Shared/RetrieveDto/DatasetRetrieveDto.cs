namespace Kritikos.Sphinx.Web.Shared.RetrieveDto
{
  using System;

  using Kritikos.Configuration.Persistence.Contracts.Behavioral;

  public class DatasetRetrieveDto : IEntity<Guid>
  {
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
  }
}
