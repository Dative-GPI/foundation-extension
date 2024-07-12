using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Admin.Abstractions;

namespace Foundation.Extension.Admin.Handlers
{
    public class PermissionApplicationsMiddleware : IMiddleware<ICoreRequest>
    {
        private IRequestContextProvider _requestContextProvider;
        public IPermissionProvider _permissionProvider;

        public PermissionApplicationsMiddleware(
            IRequestContextProvider requestContextProvider,
            IPermissionProvider permissionProvider)
        {
            _requestContextProvider = requestContextProvider;
            _permissionProvider = permissionProvider;
        }

        public async Task HandleAsync(ICoreRequest request, Func<Task> next, CancellationToken cancellationToken)
        {
            /* var context = _requestContextProvider.Context;

            var hasPermission = await _permissionProvider.HasPermissions(
                request.Authorizations.ToArray()
            );

            if (!hasPermission)
                throw new UnauthorizedAccessException(); */

            await next();
        }
    }
}