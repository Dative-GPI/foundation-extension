using System.Collections.Generic;

using XXXXX.Admin.Kernel.Models;

namespace XXXXX.Admin.Kernel
{
    public static class Actions
    {
        private static readonly ActionDefinition[] ACTIONS = new ActionDefinition[]
        {
            // Exemple
            // new ActionDefinition()
            // {
            //     Authorizations = Enumerable.Empty<string>(),
            //     LabelCode = "ui.devices.add-connected",
            //     LabelDefault = "Add connected device",
            //     Icon = "mdi-wifi",
            //     RouteTemplate = "/devices",
            //     ActionType = ActionType.OpenDrawer,
            //     ComputePath = (_, _) => Task.FromResult("/connect/devices/drawer")
            // },
        };

        public static IEnumerable<ActionDefinition> GetActions()
        {
            return ACTIONS;
        }
    }
}