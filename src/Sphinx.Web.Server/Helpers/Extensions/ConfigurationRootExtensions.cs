namespace Kritikos.Sphinx.Web.Server.Helpers.Extensions
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Microsoft.Extensions.Configuration;

  public static class ConfigurationRootExtensions
  {
    public static string GetDebugValues(this IConfigurationRoot root)
    {
      void RecurseChildren(
        StringBuilder stringBuilder,
        IEnumerable<IConfigurationSection> children,
        string indent)
      {
        foreach (var child in children)
        {
          var (value, provider) = GetValueAndProvider(root, child.Path);

          stringBuilder
            .Append(indent)
            .Append(child.Key)
            .Append('=')
            .Append(value)
            .Append(" (")
            .Append(provider)
            .AppendLine(")");

          RecurseChildren(stringBuilder, child.GetChildren(), indent + "  ");
        }
      }

      var builder = new StringBuilder();

      RecurseChildren(builder, root.GetChildren(), string.Empty);

      return builder.ToString();
    }

    private static (string? Value, IConfigurationProvider? Provider) GetValueAndProvider(
      IConfigurationRoot root,
      string key)
    {
      foreach (var provider in root.Providers.Reverse())
      {
        if (provider.TryGet(key, out var value))
        {
          return (value, provider);
        }
      }

      return (null, null);
    }
  }
}
