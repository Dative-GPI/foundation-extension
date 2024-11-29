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
        private IEntityPropertyRepository _entityPropertyRepository;
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

            _entityPropertyRepository = entityPropertiesRepository;

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

            var columns = await _columnRepository.GetMany(new ColumnsFilter()
            {
                ApplicationId = _context.ApplicationId,
                TableId = table.Id
            });

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

            // Merge columns with organisation type columns and user organisation columns
            // Keeping index & hidden from most specific to least specific
            var allowedColumns = columns
                .GroupJoin(organisationTypeColumns, tc => tc.Id, otc => otc.ColumnId, (tc, otcs) =>
                {
                    var otc = otcs.FirstOrDefault();
                    return new Column()
                    {
                        Id = tc.Id,
                        Code = tc.Code,
                        TableCode = tc.TableCode,
                        TableId = tc.TableId,
                        Value = tc.Value,
                        Label = tc.Label,
                        Sortable = tc.Sortable,
                        Filterable = tc.Filterable,
                        Index = otc?.Index ?? tc.Index,
                        Hidden = otc?.Hidden ?? tc.Hidden,
                        Disabled = tc.Disabled,
                        PropertyType = tc.PropertyType,
                        EntityPropertyId = tc.EntityPropertyId,
                        CustomPropertyId = tc.CustomPropertyId
                    };
                })
                .Where(c => !c.Disabled)
                .GroupJoin(userOrganisationColumns, c => c.Id, c => c.ColumnId, (tc, uocs) =>
                {
                    var uoc = uocs.FirstOrDefault();
                    return new Column()
                    {
                        Id = tc.Id,
                        Code = tc.Code,
                        TableCode = tc.TableCode,
                        TableId = tc.TableId,
                        Value = tc.Value,
                        Label = tc.Label,
                        Sortable = tc.Sortable,
                        Filterable = tc.Filterable,
                        Index = uoc?.Index ?? tc.Index,
                        Hidden = uoc?.Hidden ?? tc.Hidden,
                        Disabled = false, // User organisation cannot disable columns
                        PropertyType = tc.PropertyType,
                        EntityPropertyId = tc.EntityPropertyId,
                        CustomPropertyId = tc.CustomPropertyId
                    };
                }).ToList();

            var entityProperties = await _entityPropertyRepository.GetMany(new EntityPropertiesFilter()
            {
                EntityPropertiesIds = allowedColumns
                    .Where(c => c.PropertyType == PropertyType.EntityProperty)
                    .Select(c => c.EntityPropertyId.Value)
                    .Distinct()
                    .ToList()
            });

            var entityPropertyApplicationTranslations = await _translationsProvider.GetMany(
                _context.ApplicationId,
                _context.LanguageCode,
                entityProperties.Select(ep => ep.TranslationCode).Distinct().ToList()
            );

            var entityPropertyTranslations = entityProperties.GroupJoin(
                entityPropertyApplicationTranslations,
                ep => ep.TranslationCode,
                t => t.TranslationCode,
                (ep, ts) =>
                {
                    return new Tuple<PropertyType, Guid, List<TranslationItemProperty>>(
                        PropertyType.EntityProperty,
                        ep.Id,
                        ts.Select(t => new TranslationItemProperty()
                        {
                            Label = t.Value,
                            LanguageCode = t.LanguageCode
                        }).ToList()
                    );
                }).ToList();

            var translatedColumns = allowedColumns
                .Join(
                    entityPropertyTranslations,
                    ac => (ac.PropertyType, ac.CustomPropertyId ?? ac.EntityPropertyId),
                    t => (t.Item1, t.Item2),
                    (ac, t) => new CompleteUserOrganisationColumnInfos()
                    {
                        ColumnId = ac.Id,
                        Filterable = ac.Filterable,
                        Hidden = ac.Hidden,
                        Index = ac.Index,
                        Label = ac.Label,
                        Sortable = ac.Sortable,
                        Value = ac.Value,
                        Translations = t.Item3
                    }
                ).ToList();

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
                    Columns = translatedColumns
                };
            }

            return new UserOrganisationTableDetails()
            {
                Id = Guid.NewGuid(),
                Code = table.Code,
                Mode = null,
                RowsPerPage = 10,
                SortByKey = null,
                SortByOrder = null,
                Columns = translatedColumns
            };
        }
    }
}