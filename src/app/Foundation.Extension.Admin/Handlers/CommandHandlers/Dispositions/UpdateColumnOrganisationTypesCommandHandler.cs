using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpdateColumnOrganisationTypesCommandHandler : IMiddleware<UpdateOrganisationTypeTableCommand>
    {
        private ITableRepository _tableRepository;
        private IOrganisationTypeDispositionRepository _organisationTypeDispositionRepository;

        public UpdateColumnOrganisationTypesCommandHandler(
            ITableRepository tableRepository,
            IOrganisationTypeDispositionRepository columnOrganisationTypeRepository)
        {
            _tableRepository = tableRepository;
            _organisationTypeDispositionRepository = columnOrganisationTypeRepository;
        }

        public async Task HandleAsync(UpdateOrganisationTypeTableCommand request, Func<Task> next, CancellationToken cancellationToken)
        {
            var formers = await _organisationTypeDispositionRepository.GetMany(new Domain.Repositories.Filters.ColumnOrganisationTypesFilter()
            {
                OrganisationTypeId = request.OrganisationTypeId,
                TableId = request.TableId
            });

            // Remove the former columns from the given table
            await _organisationTypeDispositionRepository.RemoveMany(formers.Select(f => f.Id));

            // Recreate the new columns
            await _organisationTypeDispositionRepository.CreateMany(request.OrganisationTypeDispositions.Select(c => new CreateOrganisationTypeDisposition()
            {
                ColumnId = c.ColumnId,
                Disabled = c.Disabled,
                Hidden = c.Hidden,
                Index = c.Index,
                OrganisationTypeId = request.OrganisationTypeId
            }));
        }
    }
}