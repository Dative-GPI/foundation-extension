using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Repositories.Commands;
using Foundation.Extension.Domain.Repositories.Filters;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class UpsertPermissionOrganisationTypesCommandHandler : IMiddleware<UpsertPermissionOrganisationTypesCommand>
    {
        private IPermissionOrganisationTypeRepository _permissionOrganisationTypeRepository;

        public UpsertPermissionOrganisationTypesCommandHandler(IPermissionOrganisationTypeRepository permissionOrganisationTypeRepository)
        {
            _permissionOrganisationTypeRepository = permissionOrganisationTypeRepository;
        }

        public async Task HandleAsync(UpsertPermissionOrganisationTypesCommand request, Func<Task> next, CancellationToken cancellationToken)
        {
            var filter = new PermissionOrganisationTypesFilter()
            {
                OrganisationTypeIds = request.PermissionOrganisationTypes.Select(ot => ot.OrganisationTypeId)
            };

            // Get former permissions
            var formerIds = await _permissionOrganisationTypeRepository.GetMany(filter);

            // Remove former permissions
            await _permissionOrganisationTypeRepository.RemoveRange(formerIds.Select(p => p.Id).ToArray());

            // Create the new permissions
            await _permissionOrganisationTypeRepository.CreateRange(request.PermissionOrganisationTypes
                .SelectMany(ps => ps.PermissionIds
                    .Select(p => new CreatePermissionOrganisationType()
                    {
                        OrganisationTypeId = ps.OrganisationTypeId,
                        PermissionId = p
                    })
                )
            );
        }
    }
}