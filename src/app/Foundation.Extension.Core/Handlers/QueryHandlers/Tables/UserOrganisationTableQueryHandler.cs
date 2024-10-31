using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Core.Abstractions;
using Foundation.Extension.Core.Models;
using Foundation.Extension.Domain.Enums;
using Foundation.Extension.CrossCutting.Helpers;
using Foundation.Extension.Domain.Abstractions;

namespace Foundation.Extension.Core.Handlers
{
	public class UserOrganisationTableQueryHandler : IMiddleware<UserOrganisationTableQuery, UserOrganisationTableDetails>
	{
		private RequestContext _context;
		private ITableRepository _tableRepository;
		private IColumnRepository _columnRepository;
		private IEntityPropertyRepository _entityPropertiesRepository;
		private IOrganisationTypeDispositionRepository _organisationTypeDispositionRepository;
		private IUserOrganisationTableRepository _userOrganisationTableRepository;
		private IUserOrganisationColumnRepository _userOrganisationColumnRepository;
		private ITranslationsProvider _translationsProvider;

		public UserOrganisationTableQueryHandler
		(
			IRequestContextProvider requestContextProvider,
			ITableRepository tableRepository,
			IColumnRepository columnRepository,
			IEntityPropertyRepository entityPropertiesRepository,
			IOrganisationTypeDispositionRepository organisationTypeDispositionRepository,
			IUserOrganisationTableRepository userOrganisationTableRepository,
			IUserOrganisationColumnRepository userOrganisationColumnRepository,
			ITranslationsProvider translationsProvider)
		{
			_context = requestContextProvider.Context;

			_tableRepository = tableRepository;
			_columnRepository = columnRepository;

			_entityPropertiesRepository = entityPropertiesRepository;

			_organisationTypeDispositionRepository = organisationTypeDispositionRepository;
			_userOrganisationTableRepository = userOrganisationTableRepository;
			_userOrganisationColumnRepository = userOrganisationColumnRepository;

			_translationsProvider = translationsProvider;
		}

		public async Task<UserOrganisationTableDetails> HandleAsync(UserOrganisationTableQuery request, Func<Task<UserOrganisationTableDetails>> next, CancellationToken cancellationToken)
		{
			Table table = await _tableRepository.GetFromCode(request.TableCode);

			if (table == null)
			{
				throw new Exception(ErrorCode.EntityNotFound);
			}

			var entitiesProperties = await _entityPropertiesRepository.GetMany(new EntityPropertiesFilter()
			{
				ApplicationId = _context.ApplicationId,
				EntityType = table.EntityType
			});

			var translations = await _translationsProvider.GetMany(
				_context.ApplicationId, 
				_context.LanguageCode, 
				entitiesProperties.Select(ep => ep.TranslationCode).Distinct().ToList()
			);

			var entityPropertiesTranslations = translations.GroupJoin(
				entitiesProperties,
				t => t.TranslationCode,
				ep => ep.TranslationCode,
				(t, ep) => new
				{
					EntityPropertyId = ep.FirstOrDefault()?.Id,
					Label = t.Value
				}
			).ToList();

			var columns = await _columnRepository.GetMany(new ColumnsFilter()
			{
				ApplicationId = _context.ApplicationId,
				TableId = table.Id
			});


			var translatedColumns = columns.GroupJoin(
				entityPropertiesTranslations,
				c => c.EntityPropertyId,
				ept => ept.EntityPropertyId,
				(c, ept) => new
				{
					Column = c,
					Label = ept.FirstOrDefault()?.Label ?? c.Label
				}
			).ToList();

			var organisationTypeColumns = await _organisationTypeDispositionRepository.GetMany(new ColumnOrganisationTypesFilter()
			{
				OrganisationTypeId = _context.OrganisationTypeId,
				TableId = table.Id
			});

			var userOrganisationColumns = await _userOrganisationColumnRepository.GetMany(new UserOrganisationColumnsFilter()
			{
				UserOrganisationId = _context.ActorOrganisationId.Value,
				TableId = table.Id
			});

			// Merge translated columns with organisation type columns and user organisation columns
			// Keeping index & hidden from most specific to least specific
			var completeColumns = translatedColumns
				.GroupJoin(organisationTypeColumns, tc => tc.Column.Id, otc => otc.ColumnId, (tc, otcs) =>
				{
					var otc = otcs.FirstOrDefault();
					return new
					{
						tc.Column.Id,
						tc.Column.Value,
						tc.Column.Sortable,
						tc.Column.Filterable,
						Index = otc?.Index ?? tc.Column.Index,
						Hidden = otc?.Hidden ?? tc.Column.Hidden,
						tc.Label,
						Disabled = otc?.Disabled ?? tc.Column.Disabled
					};
				})
				.Where(c => !c.Disabled)
				.GroupJoin(userOrganisationColumns, c => c.Id, c => c.ColumnId, (tc, uocs) =>
				{
					var uoc = uocs.FirstOrDefault();
					return new CompleteUserOrganisationColumnInfos()
					{
						ColumnId = tc.Id,
						Value = tc.Value,
						Sortable = tc.Sortable,
						Filterable = tc.Filterable,
						Index = uoc?.Index ?? tc.Index,
						Hidden = uoc?.Hidden ?? tc.Hidden,
						Label = tc.Label
					};
				}).ToList();

			var userOrganisationTable = await _userOrganisationTableRepository.Find(table.Code, _context.ActorOrganisationId.Value);

			if (userOrganisationTable != null)
			{
				return new UserOrganisationTableDetails()
				{
					Id = userOrganisationTable.Id,
					Code = table.Code,
					Mode = userOrganisationTable.Mode,
					RowsPerPage = userOrganisationTable.RowsPerPage,
					SortByKey = userOrganisationTable.SortByKey,
					SortByOrder = userOrganisationTable.SortByOrder,
					Columns = completeColumns
				};
			}
			else
			{
				return new UserOrganisationTableDetails()
				{
					Id = Guid.NewGuid(),
					Code = table.Code,
					Mode = "table",
					RowsPerPage = 10,
					SortByKey = null,
					SortByOrder = null,
					Columns = completeColumns
				};
			}
		}
	}
}