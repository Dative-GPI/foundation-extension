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
            new ActionDefinition()
            {
                Authorizations = Array.Empty<string>(),
                LabelCode = "ui.devices.action",
                LabelDefault = "Action",
                Icon = "mdi-wifi",
                RouteTemplate = "/organisations/{organisationId}/device-organisations",
                ActionType = ActionType.OpenDrawer,
                ComputePath = (dico, sp) =>
                {
                    return Task.FromResult($"/organisations/{Guid.Parse(dico["organisationId"])}/XXXXX/examples/action-dialog");
                }
            },
            new ActionDefinition()
            {
                LabelCode = "ui.device.documentation-link",
                LabelDefault = "Documentation",
                Icon = "mdi-information-outline",
                RouteTemplate = "/organisations/{organisationId}/device-organisations/{deviceOrganisationId}",
                ActionType = ActionType.OpenTabs,
                Uri = "https://doc.XXXXX.fr",
                ComputePath = async (dico, sp) =>
                {
                    var ctx = sp.GetRequiredService<IRequestContextProvider>().Context;
                    var client = await sp.GetRequiredService<IFoundationClientFactory>().CreateAuthenticated(ctx.ApplicationId, ctx.LanguageCode, ctx.Jwt);
                    var device = await client.Core.DeviceOrganisations.Get(Guid.Parse(dico["deviceOrganisationId"]), ctx.OrganisationId.Value);
                    return $"{device.Code}&{device.ArticleCode}";
                }
            }
        };

		public static IEnumerable<ActionDefinition> GetActions()
		{
			return ACTIONS;
		}
	}
}