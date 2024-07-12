using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bones.Flow;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Admin.ViewModels;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Services
{
    public class OrganisationTypeTableService : IOrganisationTypeTableService
    {
        private IRequestContextProvider _requestContextProvider;
        private IOrganisationTypeTableProvider _organisationTypeTableProvider;
        private IQueryHandler<OrganisationTypeTableQuery, OrganisationTypeTableDetails> _organisationTypeTableQueryHandler;
        private ICommandHandler<UpdateOrganisationTypeTableCommand> _updateOrganisationTypeTableCommand;
        private IOrganisationTypeDispositionRepository _columnOrganisationTypeRepository;
        private IMapper _mapper;

        public OrganisationTypeTableService(
            IRequestContextProvider requestContextProvider,
            IOrganisationTypeTableProvider organisationTypeTableProvider,
            IQueryHandler<OrganisationTypeTableQuery, OrganisationTypeTableDetails> organisationTypeTableQueryHandler,
            ICommandHandler<UpdateOrganisationTypeTableCommand> updateOrganisationTypeTableCommand,
            IOrganisationTypeDispositionRepository columnOrganisationTypeRepository,
            IMapper mapper
        )
        {
            _requestContextProvider = requestContextProvider;
            _organisationTypeTableProvider = organisationTypeTableProvider;
            _organisationTypeTableQueryHandler = organisationTypeTableQueryHandler;
            _updateOrganisationTypeTableCommand = updateOrganisationTypeTableCommand;
            _columnOrganisationTypeRepository = columnOrganisationTypeRepository;
            _mapper = mapper;
        }

        public async Task<OrganisationTypeTableDetailsViewModel> Get(Guid organisationTypeId, Guid tableId)
        {
            var context = _requestContextProvider.Context;

            var query = new OrganisationTypeTableQuery()
            {
                ApplicationId = context.ApplicationId,
                ActorId = context.ActorId,
                OrganisationTypeId = organisationTypeId,
                TableId = tableId
            };

            var result = await _organisationTypeTableQueryHandler.HandleAsync(query);
            return _mapper.Map<OrganisationTypeTableDetails, OrganisationTypeTableDetailsViewModel>(result);
        }

        public async Task<OrganisationTypeTableDetailsViewModel> Update(Guid organisationTypeId, Guid tableId, UpdateOrganisationTypeTableViewModel payload)
        {
            var context = _requestContextProvider.Context;

            var command = new UpdateOrganisationTypeTableCommand()
            {
                ApplicationId = context.ApplicationId,
                ActorId = context.ActorId,
                OrganisationTypeId = organisationTypeId,
                TableId = tableId,
                OrganisationTypeDispositions = payload.Dispositions.Select(u => new UpdateOrganisationTypeDisposition()
                {
                    ColumnId = u.ColumnId,
                    Disabled = u.Disabled,
                    Index = u.Index,
                    Hidden = u.Hidden
                })
            };

            await _updateOrganisationTypeTableCommand.HandleAsync(command);

            var result = await _organisationTypeTableProvider.Get(organisationTypeId, tableId);

            return _mapper.Map<OrganisationTypeTableDetails, OrganisationTypeTableDetailsViewModel>(result);
        }
    }
}