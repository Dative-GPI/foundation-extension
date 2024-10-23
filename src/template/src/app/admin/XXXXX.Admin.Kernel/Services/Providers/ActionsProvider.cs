using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Domain.Abstractions;
using Foundation.Extension.Domain.Models;

using XXXXX.Admin.Kernel.Models;

namespace XXXXX.Admin.Kernel.Services
{
	public class ActionsProvider : IActionsProvider
	{
		private IRequestContextProvider _requestContextProvider;
		private IPermissionProvider _permissionProvider;
		private ITranslationsProvider _translationsProvider;
		private IServiceProvider _serviceProvider;
		private IExtensionMatcher _templateMatcher;

		public ActionsProvider(
			IRequestContextProvider requestContextProvider,
			IPermissionProvider permissionProvider,
			ITranslationsProvider translationsProvider,
			IServiceProvider serviceProvider,
			IExtensionMatcher templateMatcher
		)
		{
			_requestContextProvider = requestContextProvider;
			_permissionProvider = permissionProvider;
			_translationsProvider = translationsProvider;
			_serviceProvider = serviceProvider;
			_templateMatcher = templateMatcher;
		}


		public async Task<IEnumerable<ActionInfos>> GetActions(string currentPath)
		{
			var context = _requestContextProvider.Context;
			var permissions = await _permissionProvider.GetPermissions();

			var actions = Actions.GetActions();

			var allowedActions = actions.Where(r => HasPermissions(r, permissions)).ToList();

			if (!allowedActions.Any()) return Enumerable.Empty<ActionInfos>();

			var translations = await _translationsProvider.GetMany(
				context.ApplicationId,
				context.LanguageCode);

			var temp = new List<(ActionDefinition Action, string Path)>();

			foreach (var action in allowedActions)
			{
				if (_templateMatcher.TryMatch(action.RouteTemplate, currentPath, out var matches))
				{
					var path = await action.ComputePath(matches, _serviceProvider);
					temp.Add((action, path));
				}
			}

			return temp
				.GroupJoin(translations, a => a.Action.LabelCode, t => t.TranslationCode,
				(a, t) => new ActionInfos
				{
					ActionType = a.Action.ActionType,
					Icon = a.Action.Icon,
					Label = t.FirstOrDefault()?.Value ?? a.Action.LabelDefault,
					Path = a.Path,
					Uri = a.Action.Uri
				});
		}

		private bool HasPermissions(ActionDefinition action, IEnumerable<string> grantedPermissions)
		{
			if (action.Authorizations == null) return true;
			return !action.Authorizations.Except(grantedPermissions).Any();
		}
	}
}