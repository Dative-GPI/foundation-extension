using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Foundation.Extension.Core.Handlers;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.DI
{
	public static class WidgetTemplateInjector
	{
		public static IServiceCollection AddWidgetTemplates(this IServiceCollection services)
		{
			services.AddScoped<WidgetTemplatesQueryHandler>();
			services.AddScoped<IQueryHandler<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>>>(sp =>
			{
				var pipeline = sp.GetPipelineFactory<WidgetTemplatesQuery, IEnumerable<WidgetTemplateInfos>>()
					.With<PermissionsMiddleware>()
					.Add<WidgetTemplatesQueryHandler>()
					.Build();

				return pipeline;
			});

			return services;
		}
	}
}