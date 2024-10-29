using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Core.Abstractions;

using XXXXX.Core.Kernel.Models;

namespace XXXXX.Core.Kernel.Services.Providers
{
    public class RoutesProvider : IRoutesProvider
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly ITranslationsProvider _translationsProvider;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IPageRepository _pageRepository;

        public RoutesProvider
        (
            IPageRepository pageRepository,
            IPermissionProvider permissionProvider,
            ITranslationsProvider translationsProvider,
            IRequestContextProvider requestContextProvider
        )
        {
            _permissionProvider = permissionProvider;
            _translationsProvider = translationsProvider;
            _requestContextProvider = requestContextProvider;
            _pageRepository = pageRepository;
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

            var pages = await _pageRepository.GetMany(new PagesFilter());

            return routes.GroupJoin(
                translations,
                r => r.DrawerCategoryCode,
                t => t.TranslationCode,
                (r, t) => new
                {
                    Route = r,
                    Category = t.FirstOrDefault()?.Value ?? r.DrawerCategoryLabelDefault
                }
            ).GroupJoin(
                translations,
                rc => rc.Route.DrawerLabelCode,
                t => t.TranslationCode,
                (rc, t) => new RouteInfos()
                {
                    DrawerCategory = rc.Category,
                    DrawerIcon = rc.Route.DrawerIcon,
                    DrawerLabel = t.FirstOrDefault()?.Value ?? rc.Route.DrawerLabelDefault,
                    Exact = rc.Route.Exact,
                    Name = pages.FirstOrDefault(p => p.Code == rc.Route.Name)?.Id.ToString(),
                    Path = rc.Route.Path(context),
                    ShowOnDrawer = rc.Route.ShowOnDrawer
                }
            ).ToList();
        }
    }
}
