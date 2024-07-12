using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Admin.Abstractions;
using Foundation.Extension.Domain.Models;
using Foundation.Extension.Domain.Repositories.Interfaces;

namespace Foundation.Extension.Admin.Handlers
{
    public class OrganisationTypeTableQueryHandler : IMiddleware<OrganisationTypeTableQuery, OrganisationTypeTableDetails>
    {
        private IOrganisationTypeTableProvider _organisationTypeTableProvider;

        public OrganisationTypeTableQueryHandler(
            IOrganisationTypeTableProvider organisationTypeTableProvider)
        {
            _organisationTypeTableProvider = organisationTypeTableProvider;
        }

        public async Task<OrganisationTypeTableDetails> HandleAsync(OrganisationTypeTableQuery request, Func<Task<OrganisationTypeTableDetails>> next, CancellationToken cancellationToken)
        {
            return await _organisationTypeTableProvider.Get(request.OrganisationTypeId, request.TableId);
        }
    }
}