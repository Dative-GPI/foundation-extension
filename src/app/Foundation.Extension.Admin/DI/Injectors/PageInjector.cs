using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;

using Foundation.Extension.Admin.Handlers;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Admin.DI
{
	public static class PageInjector
	{
		public static IServiceCollection AddPages(this IServiceCollection services)
		{
			services.AddScoped<PagesQueryHandler>();
			services.AddScoped<IQueryHandler<PagesQuery, IEnumerable<Page>>>(sp =>
			{
				var pipeline = sp.GetPipelineFactory<PagesQuery, IEnumerable<Page>>()
					.With<PermissionApplicationsMiddleware>()
					.Add<PagesQueryHandler>()
					.Build();

				return pipeline;
			});

			return services;
		}
	}
}