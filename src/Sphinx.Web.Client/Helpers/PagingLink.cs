namespace Kritikos.Sphinx.Web.Client.Helpers
{
  public class PagingLink
  {
    public PagingLink(int page, bool enabled, string text)
    {
      Page = page;
      Enabled = enabled;
      Text = text;
    }

    public string Text { get; init; } = string.Empty;

    public int Page { get; init; }

    public bool Enabled { get; init; }

    public bool Active { get; init; }
  }
}
