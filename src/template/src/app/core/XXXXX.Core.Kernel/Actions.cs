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
                LabelCode = "ui.devices.add-connected",
                LabelDefault = "Add connected device",
                Icon = "mdi-wifi",
                RouteTemplate = "/devices",
                ActionType = ActionType.OpenDrawer,
                ComputePath = (_, _) => Task.FromResult("/connect/devices/drawer")
            },
            new ActionDefinition()
            {
                LabelCode = "ui.device.documentation-link",
                LabelDefault = "Documentation",
                Icon = "mdi-information-outline",
                RouteTemplate = "/explorer-device/device/{deviceId}",
                ActionType = ActionType.OpenTabs,
                Uri = "https://doc.XXXXX.fr",
                ComputePath = async (dico, sp) => {
                    var ctx = sp.GetRequiredService<IRequestContextProvider>().Context;
                    var client = await sp.GetRequiredService<IFoundationClientFactory>().CreateAuthenticated(ctx.ApplicationId, ctx.LanguageCode, ctx.Jwt);
                    var device = await client.Core.DeviceOrganisations.Get(ctx.OrganisationId.Value, Guid.Parse(dico["deviceId"]));
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