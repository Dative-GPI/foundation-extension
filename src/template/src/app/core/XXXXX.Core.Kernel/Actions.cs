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
            //     Authorizations = Array.Empty<string>(),
            //     LabelCode = "ui.devices.action",
            //     LabelDefault = "Action",
            //     Icon = "mdi-wifi",
            //     RouteTemplate = "/organisations/{organisationId}/device-organisations",
            //     ActionType = ActionType.OpenDrawer,
            //     ComputePath = (dico, sp) =>
            //     {
            //         return Task.FromResult($"/organisations/{Guid.Parse(dico["organisationId"])}/XXXXX/examples/action-dialog");
            //     }
            // },
        };

		public static IEnumerable<ActionDefinition> GetActions()
		{
			return ACTIONS;
		}
	}
}