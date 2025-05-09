using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Foundation.Shared;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Abstractions;

using XXXXX.Core.Kernel.Models;

namespace XXXXX.Core.Kernel
{
    public static class Actions
    {
        private static readonly ActionDefinition[] ACTIONS = new ActionDefinition[]
        {
            // Example
            // new ActionDefinition()
            // {
            //     LabelCode = "ui.action.documentation-link",
            //     LabelDefault = "Documentation",
            //     Icon = "mdi-information-outline",
            //     Color = "#00D000",
            //     RouteTemplate = "/organisations/{organisationId}/device-organisations/{deviceOrganisationId}",
            //     ActionType = ActionType.OpenTabs,
            //     Uri = "https://doc.bongard.fr",
            //     ComputePath = async (dico, sp) =>
            //     {
            //         var ctx = sp.GetRequiredService<IRequestContextProvider>().Context;
            //         var client = await sp.GetRequiredService<IFoundationClientFactory>().CreateAuthenticated(ctx.ApplicationId, ctx.LanguageCode, ctx.Jwt);
            //         var device = await client.Core.DeviceOrganisations.Get(Guid.Parse(dico["deviceOrganisationId"]), ctx.OrganisationId.Value);
            //         return $"{device.Code}&{device.ArticleCode}";
            //     }
            // }
        };

		public static IEnumerable<ActionDefinition> GetActions()
		{
			return ACTIONS;
		}
	}
}