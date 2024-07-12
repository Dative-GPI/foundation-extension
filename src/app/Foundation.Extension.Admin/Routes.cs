using System.Collections.Generic;
using Foundation.Extension.Admin.Models;

namespace Foundation.Extension.Admin
{
    public static class Routes
    {
        private static readonly RouteDefinition[] ROUTES = new RouteDefinition[] {
            new RouteDefinition()
            {
                Authorizations = new string[] {},
                Path = (ctx) => "/v1/application-translations",
                Name = "apps.foundation-template.drawer",
                DrawerCategoryLabelDefault = null,
                DrawerCategoryCode = null,
                DrawerIcon = null,
                DrawerLabelDefault = null,
                DrawerLabelCode = null,
                Exact = false,
                ShowOnDrawer = false
            },
            new RouteDefinition()
            {
                Authorizations = new string[] {},
                Path = (ctx) => "/v1/entities",
                Name = "apps.foundation-template.drawer",
                DrawerCategoryLabelDefault = null,
                DrawerCategoryCode = null,
                DrawerIcon = null,
                DrawerLabelDefault = null,
                DrawerLabelCode = null,
                Exact = false,
                ShowOnDrawer = false
            },
            new RouteDefinition()
            {
                Authorizations = new string[] {},
                Path = (ctx) => "/v1/role-applications/:roleId",
                Name = "apps.foundation-template.drawer",
                DrawerCategoryLabelDefault = null,
                DrawerCategoryCode = null,
                DrawerIcon = null,
                DrawerLabelDefault = null,
                DrawerLabelCode = null,
                Exact = false,
                ShowOnDrawer = false
            },
        };


        public static IEnumerable<RouteDefinition> GetRoutes()
        {
            return ROUTES;
        }
    }
}