using System;
using System.Threading;
using System.Threading.Tasks;

using Bones.Flow;

using Foundation.Extension.Domain.Repositories.Interfaces;
using Foundation.Extension.Gateway.Requests.Commands;

namespace Foundation.Extension.Gateway.Handlers
{
    public class RemoveApplicationCommandHandler : IMiddleware<RemoveApplicationCommand>
    {
        private IApplicationRepository _applicationRepository;

        public RemoveApplicationCommandHandler(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task HandleAsync(RemoveApplicationCommand request, Func<Task> next, CancellationToken cancellationToken)
        {
            await _applicationRepository.Remove(request.ApplicationId);
        }
    }
}