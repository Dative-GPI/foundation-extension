using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Bones.Flow;

using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using AutoMapper;
using System.Linq;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Services
{
    public class TableService : ITableService
    {
        private readonly IQueryHandler<TableQuery, ApplicationTableDetails> _tableQueryHandler;
        private readonly IQueryHandler<TablesQuery, IEnumerable<ApplicationTableInfos>> _tablesQueryHandler;
        private readonly ICommandHandler<PatchTableCommand> _patchTableCommandHandler;
        private readonly ICommandHandler<UpdateTableCommand> _updateTableCommandHandler;
        private readonly IRequestContextProvider _requestContextProvider;
        private readonly IApplicationTableProvider _applicationTableProvider;
        private readonly IMapper _mapper;

        public TableService
        (
            IQueryHandler<TableQuery, ApplicationTableDetails> tableQueryHandler,
            IQueryHandler<TablesQuery, IEnumerable<ApplicationTableInfos>> tablesQueryHandler,
            ICommandHandler<PatchTableCommand> patchTableCommandHandler,
            ICommandHandler<UpdateTableCommand> updateTableCommandHandler,
            IRequestContextProvider requestContextProvider,
            IApplicationTableProvider applicationTableProvider,
            IMapper mapper
        )
        {
            _tableQueryHandler = tableQueryHandler;
            _tablesQueryHandler = tablesQueryHandler;
            _patchTableCommandHandler = patchTableCommandHandler;
            _updateTableCommandHandler = updateTableCommandHandler;
            _requestContextProvider = requestContextProvider;
            _applicationTableProvider = applicationTableProvider;
            _mapper = mapper;
        }

        public async Task<ApplicationTableDetailsViewModel> Get(Guid tableId)
        {
            var context = _requestContextProvider.Context;
            var query = new TableQuery()
            {
                ActorId = context.ActorId,
                ApplicationId = context.ApplicationId,
                TableId = tableId
            };

            var result = await _tableQueryHandler.HandleAsync(query);

            return _mapper.Map<ApplicationTableDetails, ApplicationTableDetailsViewModel>(result);
        }

        public async Task<IEnumerable<ApplicationTableInfosViewModel>> GetMany(TableFiltersViewModel filter)
        {
            var context = _requestContextProvider.Context;
            var query = new TablesQuery()
            {
                ActorId = context.ActorId,
                ApplicationId = context.ApplicationId
            };

            var result = await _tablesQueryHandler.HandleAsync(query);

            return _mapper.Map<IEnumerable<ApplicationTableInfos>, IEnumerable<ApplicationTableInfosViewModel>>(result);
        }

        public async Task<ApplicationTableDetailsViewModel> Patch(Guid tableId)
        {
            var context = _requestContextProvider.Context;
            var command = new PatchTableCommand()
            {
                ActorId = context.ActorId,
                ApplicationId = context.ApplicationId,
                TableId = tableId
            };

            await _patchTableCommandHandler.HandleAsync(command);

            var table = await _applicationTableProvider.Get(tableId);

            return _mapper.Map<ApplicationTableDetails, ApplicationTableDetailsViewModel>(table);
        }

        public async Task<ApplicationTableDetailsViewModel> Update(Guid tableId, UpdateTableViewModel payload)
        {
            var context = _requestContextProvider.Context;
            var command = new UpdateTableCommand()
            {
                ActorId = context.ActorId,
                ApplicationId = context.ApplicationId,
                TableId = tableId,

                Columns = payload.Columns.Select(p => new UpdateColumn()
                {
                    Id = p.Id,
                    Index = p.Index,
                    Hidden = p.Hidden,
                    Sortable = p.Sortable,
                    Filterable = p.Filterable,
                    Configurable = p.Configurable,
                    Disabled = p.Disabled
                }).ToList()
            };

            await _updateTableCommandHandler.HandleAsync(command);

            var table = await _applicationTableProvider.Get(tableId);

            return _mapper.Map<ApplicationTableDetails, ApplicationTableDetailsViewModel>(table);
        }
    }
}