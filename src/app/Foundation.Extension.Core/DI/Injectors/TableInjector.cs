using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Bones.Flow;
using Foundation.Extension.Core.Handlers;
using Foundation.Extension.Domain.Models;

namespace Foundation.Extension.Core.DI
{
  public static class TableInjector
  {
    public static IServiceCollection AddTables(this IServiceCollection services)
    {
      services.AddScoped<UserOrganisationTableQueryHandler>();
      services.AddScoped<IQueryHandler<UserOrganisationTableQuery, UserOrganisationTableDetails>>(sp =>
      {
        var pipeline = sp.GetPipelineFactory<UserOrganisationTableQuery, UserOrganisationTableDetails>()
                  // .With<PermissionsMiddleware>()
                  .Add<UserOrganisationTableQueryHandler>()
                  .Build();

        return pipeline;
      });

      services.AddScoped<UpdateUserOrganisationTableCommandHandler>();
      services.AddScoped<ICommandHandler<UpdateUserOrganisationTableCommand>>(sp =>
      {
        var pipeline = sp.GetPipelineFactory<UpdateUserOrganisationTableCommand>()
                  // .With<PermissionsMiddleware>()
                  .Add<UpdateUserOrganisationTableCommandHandler>()
                  .Build();

        return pipeline;
      });

      return services;
    }
  }
}