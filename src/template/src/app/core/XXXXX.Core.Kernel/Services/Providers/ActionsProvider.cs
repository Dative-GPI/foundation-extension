using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using XXXXX.Core.Kernel.Models;

using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;

namespace XXXXX.Core.Kernel.Services.Providers
{
    public class ActionsProvider : IActionsProvider
    {
        private readonly IPermissionProvider _permissionProvider;
        private readonly ITranslationsProvider _translationsProvider;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IExtensionMatcher _templateMatcher;
        private readonly IServiceProvider _serviceProvider;

        public ActionsProvider
        (
            IPermissionProvider permissionProvider,
            ITranslationsProvider translationsProvider,
            IRequestContextProvider requestContextProvider,
            IExtensionMatcher templateMatcher,
            IServiceProvider serviceProvider
        )
        {
            _permissionProvider = permissionProvider;
            _translationsProvider = translationsProvider;
            _requestContextProvider = requestContextProvider;
            _templateMatcher = templateMatcher;
            _serviceProvider = serviceProvider;
        }

        public async Task<IEnumerable<ActionInfos>> GetActions(string path)
        {
            var context = _requestContextProvider.Context;
            var permissions = await _permissionProvider.GetPermissions();

            var actions = Actions.GetActions();

            var allowedActions = actions.Where(action => HasPermissions(action, permissions)).ToList();

            if (!allowedActions.Any())
            {
                return Enumerable.Empty<ActionInfos>();
            }

            var translations = await _translationsProvider.GetMany(context.ApplicationId, context.LanguageCode);

            var temp = new List<(ActionDefinition Action, string Path)>();

            foreach (var action in allowedActions)
            {
                if (_templateMatcher.TryMatch(action.RouteTemplate, path, out var matches))
                {
                    var computedPath = await action.ComputePath(matches, _serviceProvider);
                    temp.Add((action, computedPath));
                }
            }

            var definitiveActions = temp.GroupJoin(translations, a => a.Action.LabelCode, t => t.TranslationCode, (a, t) => new ActionInfos()
            {
                ActionType = a.Action.ActionType,
                Icon = a.Action.Icon,
                Label = t.FirstOrDefault()?.Value ?? a.Action.LabelDefault,
                Path = a.Path,
                Uri = a.Action.Uri
            });

            return definitiveActions;
        }

        private static bool HasPermissions(ActionDefinition action, IEnumerable<string> grantedPermissions)
        {
            if (action.Authorizations == null)
            {
                return true;
            }
            return !action.Authorizations.Except(grantedPermissions).Any();
        }
    }
}