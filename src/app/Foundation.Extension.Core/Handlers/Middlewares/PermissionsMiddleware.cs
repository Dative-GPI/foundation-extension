using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;
using Foundation.Extension.Core.Abstractions;

namespace Foundation.Extension.Core.Handlers
{
    public class PermissionsMiddleware : IMiddleware<ICoreRequest>
    {
        private IRequestContextProvider _requestContextProvider;
        public IPermissionProvider _permissionProvider;

        public PermissionsMiddleware(
            IRequestContextProvider requestContextProvider,
            IPermissionProvider permissionProvider)
        {
            _requestContextProvider = requestContextProvider;
            _permissionProvider = permissionProvider;
        }

        public async Task HandleAsync(ICoreRequest request, Func<Task> next, CancellationToken cancellationToken)
        {
            var context = _requestContextProvider.Context;

            var hasPermission = await _permissionProvider.HasPermissions(
                request.Authorizations.ToArray()
            );

            if (!hasPermission)
                throw new UnauthorizedAccessException();

            await next();
        }
    }
}