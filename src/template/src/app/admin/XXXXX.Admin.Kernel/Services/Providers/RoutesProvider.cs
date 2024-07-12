using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Admin.Abstractions;

using XXXXX.Admin.Kernel.Models;

namespace XXXXX.Admin.Kernel.Services
{
  public class RoutesProvider : IRoutesProvider
  {
    private readonly IPermissionProvider _permissionProvider;
    private readonly ITranslationsProvider _translationsProvider;
    private readonly IRequestContextProvider _requestContextProvider;

    public RoutesProvider(
        IPermissionProvider permissionProvider,
        ITranslationsProvider translationsProvider,
        IRequestContextProvider requestContextProvider
    )
    {
      _permissionProvider = permissionProvider;
      _translationsProvider = translationsProvider;
      _requestContextProvider = requestContextProvider;
    }

    public async Task<IEnumerable<RouteInfos>> GetRoutes()
    {
      var context = _requestContextProvider.Context;

      var permissions = await _permissionProvider.GetPermissions();
      var routes = Routes.GetRoutes();

      var allowedRoutes = routes.Where(r => HasPermissions(r, permissions)).ToList();
      var translatedAllowedRoutes = await TranslateRoutesAsync(allowedRoutes);
      return translatedAllowedRoutes;
    }

    private bool HasPermissions(RouteDefinition route, IEnumerable<string> permissions)
    {
      return !route.Authorizations.Except(permissions).Any();
    }

    private async Task<IEnumerable<RouteInfos>> TranslateRoutesAsync(IEnumerable<RouteDefinition> routes)
    {
      var context = _requestContextProvider.Context;

      var translations = await _translationsProvider.GetMany(
          context.ApplicationId,
          context.LanguageCode
      );

      return routes.GroupJoin(
              translations,
              r => r.DrawerCategoryCode,
              t => t.TranslationCode,
              (r, t) => new
              {
                Route = r,
                Category = t.FirstOrDefault()?.Value ?? r.DrawerCategoryLabelDefault
              }
          )
          .GroupJoin(
              translations,
              rc => rc.Route.DrawerLabelCode,
              t => t.TranslationCode,
              (rc, t) => new RouteInfos()
              {
                DrawerCategory = rc.Category,
                DrawerIcon = rc.Route.DrawerIcon,
                DrawerLabel = t.FirstOrDefault()?.Value ?? rc.Route.DrawerLabelDefault,
                Exact = rc.Route.Exact,
                Name = rc.Route.Name,
                Path = rc.Route.Path,
                ShowOnDrawer = rc.Route.ShowOnDrawer
              }
          )
          .ToList();
    }
  }
}
