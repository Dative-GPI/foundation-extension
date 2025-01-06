using System.Collections.Generic;
using XXXXX.Core.Kernel.Models;

namespace XXXXX.Core.Kernel
{
	public static class Routes
	{
		private static readonly RouteDefinition[] ROUTES = new RouteDefinition[] {
			new RouteDefinition()
			{
				Authorizations = new string[] {},
				Path = (ctx) => $"/organisations/{ctx.OrganisationId.Value}/XXXXX/examples",
				Name = "apps.example",
				DrawerCategoryLabelDefault = "XXXXX",
				DrawerCategoryCode = "drawer.examples.category",
				DrawerIcon = "supervised_user_circle",
				DrawerLabelDefault = "Example",
				DrawerLabelCode = "drawer.examples.title",
				Exact = true,
				ShowOnDrawer = true
			},
			new RouteDefinition()
			{
				Authorizations = new string[] {},
				Path = (ctx)=> $"/organisations/{ctx.OrganisationId.Value}/XXXXX/examples",
				Name = "apps.example.dialog",
				DrawerCategoryLabelDefault = null,
				DrawerCategoryCode = null,
				DrawerIcon = null,
				DrawerLabelDefault = null,
				DrawerLabelCode = null,
				Exact = false,
				ShowOnDrawer = false
			}
		};

		public static IEnumerable<RouteDefinition> GetRoutes()
		{
			return ROUTES;
		}
	}
}