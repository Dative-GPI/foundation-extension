using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Template.Domain.Models;
using Foundation.Template.Domain.Repositories.Filters;
using Foundation.Template.Domain.Repositories.Interfaces;

namespace Foundation.Template.Admin.Handlers
{
    public class PermissionApplicationsQueryHandler : IMiddleware<PermissionApplicationsQuery, IEnumerable<PermissionApplicationInfos>>
    {
        private IPermissionApplicationRepository _permissionApplicationRepository;
        
        public PermissionApplicationsQueryHandler(IPermissionApplicationRepository permissionApplicationRepository)
        {
            _permissionApplicationRepository = permissionApplicationRepository;
        }

        public async Task<IEnumerable<PermissionApplicationInfos>> HandleAsync(PermissionApplicationsQuery request, Func<Task<IEnumerable<PermissionApplicationInfos>>> next, CancellationToken cancellationToken)
        {
            var filter = new PermissionApplicationFilter()
            {
                Search = request.Search
            };

            return await _permissionApplicationRepository.GetMany(filter);
        }
    }
}