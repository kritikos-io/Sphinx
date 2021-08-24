namespace Kritikos.Sphinx.Web.Server.Helpers
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.Abstractions;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Mvc.Razor;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Mvc.ViewEngines;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Extensions.Logging;

  public class RazorToStringRenderer
  {
    private const string CouldNotLocateView = "Unable to find view {ViewName} in the following paths: {Locations}";

    private readonly IRazorViewEngine viewEngine;
    private readonly ITempDataProvider tempDataProvider;
    private readonly IServiceProvider serviceProvider;

    private readonly ILogger<RazorToStringRenderer> logger;

    public RazorToStringRenderer(
      IRazorViewEngine viewEngine,
      ITempDataProvider tempDataProvider,
      IServiceProvider serviceProvider,
      ILogger<RazorToStringRenderer> logger)
    {
      this.viewEngine = viewEngine;
      this.tempDataProvider = tempDataProvider;
      this.serviceProvider = serviceProvider;
      this.logger = logger;
    }

    public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
    {
      var context = new ActionContext(
        new DefaultHttpContext { RequestServices = serviceProvider },
        new RouteData(),
        new ActionDescriptor());

      var view = FindView(context, viewName);
      using var output = new StringWriter();
      var viewContext = new ViewContext(
        context,
        view,
        new ViewDataDictionary<TModel>(
          metadataProvider: new EmptyModelMetadataProvider(),
          modelState: new ModelStateDictionary()) { Model = model, },
        new TempDataDictionary(context.HttpContext, tempDataProvider),
        output,
        new HtmlHelperOptions());

      await view.RenderAsync(viewContext);

      return output.ToString();
    }

    private IView FindView(ActionContext context, string viewName)
    {
      var viewResult = viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
      if (viewResult.Success)
      {
        return viewResult.View;
      }

      var findViewResult = viewEngine.FindView(context, viewName, isMainPage: true);
      if (findViewResult.Success)
      {
        return findViewResult.View;
      }

      var locations = viewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
      logger.LogCritical(CouldNotLocateView, viewName, locations);

      throw new InvalidOperationException($"Could not locate {viewName} in {locations}");
    }
  }
}
